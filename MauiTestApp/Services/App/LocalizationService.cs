using TemplateServices.Core.Services.App;
using static TemplateServices.Core.Helpers.Constants.LocalizationConstant;

namespace MauiTestApp.Services.App
{
	public class LocalizationService : ILocalizationService
	{
		private readonly Dictionary<string, Dictionary<string, string>> _localizedStrings;

		private string _currentCulture = "en";

		public string CurrentCulture => _currentCulture;

		/**/

		public LocalizationService()
		{
			_localizedStrings = new Dictionary<string, Dictionary<string, string>>
			{
				["en"] = new Dictionary<string, string>
				{
					["bluetooth_compatibility_error"] = "Bluetooth compatibility issue. Please check your device settings.",
					["network_connection_error"] = "Network connection problem. Please check your internet connection.",
					["permission_denied_error"] = "Permission required. Please grant the necessary permissions.",
					["file_not_found_error"] = "Required file not found. Please try again.",
					["invalid_operation_error"] = "Operation cannot be completed at this time.",
					["unknown_error"] = "An unexpected error occurred. Please try again.",
					["error_title"] = "Error",
					["ok_button"] = "OK",
					["retry_button"] = "Retry",
					["cancel_button"] = "Cancel",
					[NO_RECORDS_WERE_AFFECTED_ERROR] = "no se afectó ningún registro.",
					[DELETE_QUESTION_CONFIRMATION] = "¿Esta seguro de eliminar?",
					[REQUIRED_DYNAMIC_MESSAGE] = "El campo \"dynamicParameterKey\" es requerido.",
					[CATEGORY] = "Categoría"
				},
				["es"] = new Dictionary<string, string>
				{
					["bluetooth_compatibility_error"] = "Problema de compatibilidad de Bluetooth. Por favor revise la configuración de su dispositivo.",
					["network_connection_error"] = "Problema de conexión de red. Por favor revise su conexión a internet.",
					["permission_denied_error"] = "Permiso requerido. Por favor otorgue los permisos necesarios.",
					["file_not_found_error"] = "Archivo requerido no encontrado. Por favor intente nuevamente.",
					["invalid_operation_error"] = "La operación no puede completarse en este momento.",
					["unknown_error"] = "Ocurrió un error inesperado. Por favor intente nuevamente.",
					["error_title"] = "Error",
					["ok_button"] = "Aceptar",
					["retry_button"] = "Reintentar",
					["cancel_button"] = "Cancelar",
					[NO_RECORDS_WERE_AFFECTED_ERROR] = "no records were affected.",
					[DELETE_QUESTION_CONFIRMATION] = "¿Are you sure delete?",
					[REQUIRED_DYNAMIC_MESSAGE] = "The field \"dynamicParameterKey\" is required.",
					[CATEGORY] = "Category"
				}
			};
		}

		public string GetString(string key)
		{
			if (_localizedStrings.TryGetValue(
				_currentCulture, out Dictionary<string, string>? cultureStrings) &&
				cultureStrings.TryGetValue(key, out string? localizedString)
			)
			{
				return localizedString;
			}

			// Fallback to English if key not found in current culture
			if (_currentCulture != "en" &&
				_localizedStrings.TryGetValue("en", out Dictionary<string, string>? englishStrings) &&
				englishStrings.TryGetValue(key, out string? englishString)
			)
			{
				return englishString;
			}

			return key; // Return key as fallback
		}

		public string GetString(string key, string dynamicParameterKey)
		{
			string dynamicString = GetString(dynamicParameterKey);

			return GetString(key).Replace("dynamicParameterKey", dynamicString);
		}

		public void SetCulture(string cultureCode)
		{
			if (_localizedStrings.ContainsKey(cultureCode))
			{
				_currentCulture = cultureCode;
			}
		}
	}
}