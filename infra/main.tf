data "azurerm_client_config" "current" {}

# 4-byte hex suffix for globally-unique resource names (ACR, Key Vault, etc.)
resource "random_id" "suffix" {
  byte_length = 4
}

# Auto-generated PostgreSQL password stored in Key Vault — never in state plain text
resource "random_password" "postgres" {
  length           = 24
  special          = true
  override_special = "!#$%&*()-_=+[]{}<>:?"
}

resource "azurerm_resource_group" "main" {
  name     = "${local.name_prefix}-rg"
  location = var.location
  tags     = local.common_tags
}
