# Note: Azure OpenAI requires approved access in your subscription.
# Request access at: https://aka.ms/oai/access if apply fails with 403.
resource "azurerm_cognitive_account" "openai" {
  name                  = "${local.name_prefix}-oai"
  location              = azurerm_resource_group.main.location
  resource_group_name   = azurerm_resource_group.main.name
  kind                  = "OpenAI"
  sku_name              = "S0"
  custom_subdomain_name = "${local.name_prefix}-oai"
  tags                  = local.common_tags
}

resource "azurerm_cognitive_deployment" "gpt4_mini" {
  name                 = "gpt-4-1-mini"
  cognitive_account_id = azurerm_cognitive_account.openai.id

  model {
    format  = "OpenAI"
    name    = "gpt-4.1-mini"
    version = var.openai_model_version
  }

  scale {
    type     = "Standard"
    capacity = 10 # thousands of tokens per minute
  }
}
