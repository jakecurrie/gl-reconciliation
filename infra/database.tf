resource "azurerm_postgresql_flexible_server" "main" {
  name                   = "${local.name_prefix}-psql"
  resource_group_name    = azurerm_resource_group.main.name
  location               = var.db_location
  version                = "16"
  administrator_login    = var.postgres_admin_login
  administrator_password = random_password.postgres.result
  storage_mb             = 32768
  sku_name               = "B_Standard_B1ms"
  tags                   = local.common_tags
}

resource "azurerm_postgresql_flexible_server_database" "main" {
  name      = "glrec"
  server_id = azurerm_postgresql_flexible_server.main.id
  collation = "en_US.utf8"
  charset   = "utf8"
}

# 0.0.0.0 → 0.0.0.0 is the Azure convention for "allow all Azure-hosted services"
resource "azurerm_postgresql_flexible_server_firewall_rule" "azure_services" {
  name             = "AllowAzureServices"
  server_id        = azurerm_postgresql_flexible_server.main.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
}
