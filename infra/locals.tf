locals {
  name_prefix = "${var.prefix}-${var.environment}"

  # ACR names must be alphanumeric only, globally unique, 5-50 chars
  acr_name = "${var.prefix}${var.environment}${random_id.suffix.hex}"

  common_tags = {
    Project     = "gl-rec"
    Environment = var.environment
    ManagedBy   = "terraform"
  }
}
