resource "azurerm_servicebus_namespace" "main" {
  name                = "${local.name_prefix}-bus"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  sku                 = "Standard"
  tags                = local.common_tags
}

resource "azurerm_servicebus_queue" "classification_request" {
  name         = "gl-classification-request"
  namespace_id = azurerm_servicebus_namespace.main.id

  partitioning_enabled = true
  max_delivery_count   = 10
}

resource "azurerm_servicebus_queue" "classification_result" {
  name         = "gl-classification-result"
  namespace_id = azurerm_servicebus_namespace.main.id

  partitioning_enabled = true
  max_delivery_count   = 10
}

# Send-only rule for the .NET API (publishes requests, reads results)
resource "azurerm_servicebus_namespace_authorization_rule" "api_sender" {
  name         = "api-sender"
  namespace_id = azurerm_servicebus_namespace.main.id

  listen = true
  send   = true
  manage = false
}

# Listen + send rule for the Python worker (consumes requests, publishes results)
resource "azurerm_servicebus_namespace_authorization_rule" "worker" {
  name         = "worker"
  namespace_id = azurerm_servicebus_namespace.main.id

  listen = true
  send   = true
  manage = false
}
