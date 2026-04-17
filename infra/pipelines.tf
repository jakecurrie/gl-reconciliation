resource "azuredevops_extension" "sonarcloud" {
  publisher_id = "SonarSource"
  extension_id = "sonarcloud"
}

resource "azuredevops_project" "main" {
  name               = var.ado_project_name
  visibility         = "private"
  version_control    = "Git"
  work_item_template = "Agile"
}

# ── Service connections ───────────────────────────────────────────────────────

resource "azuredevops_serviceendpoint_github" "main" {
  project_id            = azuredevops_project.main.id
  service_endpoint_name = "github"

  auth_personal {
    personal_access_token = var.github_pat
  }
}

resource "azuredevops_serviceendpoint_azurerm" "main" {
  project_id            = azuredevops_project.main.id
  service_endpoint_name = "azure-subscription"
  environment           = "AzureCloud"

  credentials {
    serviceprincipalid  = var.azure_service_principal_id
    serviceprincipalkey = var.azure_service_principal_key
  }

  features {
    validate = false
  }

  azurerm_spn_tenantid      = data.azurerm_client_config.current.tenant_id
  azurerm_subscription_id   = data.azurerm_client_config.current.subscription_id
  azurerm_subscription_name = "Azure Subscription"
}

resource "azuredevops_serviceendpoint_azurecr" "main" {
  project_id                = azuredevops_project.main.id
  service_endpoint_name     = "acr"
  resource_group            = azurerm_resource_group.main.name
  azurecr_spn_tenantid      = data.azurerm_client_config.current.tenant_id
  azurecr_name              = azurerm_container_registry.main.name
  azurecr_subscription_id   = data.azurerm_client_config.current.subscription_id
  azurecr_subscription_name = "Azure Subscription"
}

resource "azuredevops_serviceendpoint_sonarcloud" "main" {
  project_id            = azuredevops_project.main.id
  service_endpoint_name = "sonarcloud"
  token                 = var.sonarcloud_token

  depends_on = [azuredevops_extension.sonarcloud]
}

# ── Variable group ────────────────────────────────────────────────────────────

resource "azuredevops_variable_group" "pipeline_vars" {
  project_id   = azuredevops_project.main.id
  name         = "gl-rec-pipeline-vars"
  allow_access = true

  variable {
    name  = "SONARCLOUD_ORG"
    value = var.sonarcloud_org
  }

  variable {
    name  = "ACR_SERVICE_CONNECTION"
    value = azuredevops_serviceendpoint_azurecr.main.service_endpoint_name
  }

  variable {
    name  = "AZURE_SUBSCRIPTION"
    value = azuredevops_serviceendpoint_azurerm.main.service_endpoint_name
  }
}

# ── Agent pool authorization ──────────────────────────────────────────────────

data "azuredevops_agent_pool" "default" {
  name = "Default"
}

resource "azuredevops_agent_queue" "default" {
  project_id    = azuredevops_project.main.id
  agent_pool_id = data.azuredevops_agent_pool.default.id
}

resource "azuredevops_pipeline_authorization" "default_pool" {
  project_id  = azuredevops_project.main.id
  resource_id = azuredevops_agent_queue.default.id
  type        = "queue"
  pipeline_id = azuredevops_build_definition.main.id
}

resource "azuredevops_pipeline_authorization" "hosted_pool" {
  project_id  = azuredevops_project.main.id
  resource_id = "18"
  type        = "queue"
  pipeline_id = azuredevops_build_definition.main.id
}

# ── Pipeline ──────────────────────────────────────────────────────────────────

resource "azuredevops_build_definition" "main" {
  project_id = azuredevops_project.main.id
  name       = "gl-reconciliation-ci"

  ci_trigger {
    use_yaml = true
  }

  repository {
    repo_type             = "GitHub"
    repo_id               = var.github_repo
    branch_name           = "refs/heads/main"
    yml_path              = "azure-pipelines.yml"
    service_connection_id = azuredevops_serviceendpoint_github.main.id
  }

  variable_groups = [azuredevops_variable_group.pipeline_vars.id]
}
