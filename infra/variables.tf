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
