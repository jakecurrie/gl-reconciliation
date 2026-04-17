variable "prefix" {
  description = "Short prefix used in all resource names"
  type        = string
  default     = "glrec"
}

variable "environment" {
  description = "Deployment environment (dev, staging, prod)"
  type        = string
  default     = "dev"
}

variable "location" {
  description = "Azure region. Must support Azure OpenAI — eastus and westeurope are safest."
  type        = string
  default     = "canadaeast"
}

variable "db_location" {
  description = "Azure region for PostgreSQL. Canada East is restricted on new subscriptions — eastus2 is used as fallback."
  type        = string
  default     = "eastus2"
}

variable "postgres_admin_login" {
  description = "PostgreSQL Flexible Server administrator login"
  type        = string
  default     = "pgadmin"
}

variable "openai_model_version" {
  description = "GPT-4.1-mini model version string as shown in Azure OpenAI"
  type        = string
  default     = "2025-04-14"
}

variable "acr_sku" {
  description = "Azure Container Registry SKU (Basic, Standard, Premium)"
  type        = string
  default     = "Basic"
}

# ── Azure DevOps ──────────────────────────────────────────────────────────────

variable "ado_org_url" {
  description = "Azure DevOps organisation URL, e.g. https://dev.azure.com/myorg"
  type        = string
}

variable "ado_pat" {
  description = "Azure DevOps personal access token (needs Project, Build, Service Connection scopes)"
  type        = string
  sensitive   = true
}

variable "ado_project_name" {
  description = "Azure DevOps project name"
  type        = string
  default     = "gl-reconciliation"
}

variable "github_repo" {
  description = "GitHub repo in owner/name format, e.g. jakecurrie/gl-reconciliation"
  type        = string
}

variable "github_pat" {
  description = "GitHub personal access token for the ADO service connection (repo scope)"
  type        = string
  sensitive   = true
}

variable "sonarcloud_org" {
  description = "SonarCloud organisation key"
  type        = string
}

variable "sonarcloud_token" {
  description = "SonarCloud user token"
  type        = string
  sensitive   = true
}

variable "azure_service_principal_id" {
  description = "Client ID of the service principal used for the ADO → Azure service connection"
  type        = string
}

variable "azure_service_principal_key" {
  description = "Client secret of the service principal used for the ADO → Azure service connection"
  type        = string
  sensitive   = true
}
