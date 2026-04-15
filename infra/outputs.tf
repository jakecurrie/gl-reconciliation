output "resource_group_name" {
  description = "Name of the provisioned resource group"
  value       = azurerm_resource_group.main.name
}

output "container_registry_login_server" {
  description = "ACR login server (used in docker push / pipeline config)"
  value       = azurerm_container_registry.main.login_server
}

output "container_app_environment_id" {
  description = "Resource ID of the Container App Environment"
  value       = azurerm_container_app_environment.main.id
}

output "postgres_server_fqdn" {
  description = "Fully-qualified domain name of the PostgreSQL Flexible Server"
  value       = azurerm_postgresql_flexible_server.main.fqdn
}

output "openai_endpoint" {
  description = "Azure OpenAI endpoint (also stored in Key Vault)"
  value       = azurerm_cognitive_account.openai.endpoint
}

output "openai_deployment_name" {
  description = "Name of the GPT-4.1-mini deployment — pass as AZURE_OPENAI_DEPLOYMENT"
  value       = azurerm_cognitive_deployment.gpt4_mini.name
}

output "servicebus_namespace_name" {
  description = "Service Bus namespace name"
  value       = azurerm_servicebus_namespace.main.name
}

output "key_vault_uri" {
  description = "Key Vault URI — set as AZURE_KEY_VAULT_URI in container config"
  value       = azurerm_key_vault.main.vault_uri
}

output "key_vault_name" {
  description = "Key Vault name"
  value       = azurerm_key_vault.main.name
}
