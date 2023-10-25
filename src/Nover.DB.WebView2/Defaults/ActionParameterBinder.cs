using Nover.DB.WebView2.Infrastructure;
using Nover.DB.WebView2.Network;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;

namespace Nover.DB.WebView2.Defaults
{
    /// <summary>
    /// The default implementation of <see cref="IActionParameterBinder"/>.
    /// </summary>
    public class ActionParameterBinder : IActionParameterBinder
    {
        protected readonly IDataTransferOptions _dataTransfers;
        protected readonly ILogger<ActionParameterBinder> _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="ActionParameterBinder"/>.
        /// </summary>
        /// <param name="dataTransfers">The <see cref="IDataTransferOptions"/> instance.</param>
        public ActionParameterBinder(IDataTransferOptions dataTransfers, ILogger<ActionParameterBinder> logger)
        {
            _dataTransfers = dataTransfers;
            _logger = logger;
        }

        /// <inheritdoc />
        public virtual object Bind(string parameterName, Type type, JsonElement value)
        {
            try
            {
                TypeCode typeCode = Type.GetTypeCode(type);

                switch (typeCode)
                {
                    case TypeCode.Empty:
                    case TypeCode.DBNull:
                        return type.DefaultValue();

                    case TypeCode.Boolean:
                        return value.GetBoolean();

                    case TypeCode.Char:
                        return value.GetString()[0];

                    case TypeCode.SByte:
                        return value.GetSByte();

                    case TypeCode.Byte:
                        return value.GetByte();

                    case TypeCode.Int16:
                        return value.GetInt16();

                    case TypeCode.UInt16:
                        return value.GetUInt16();

                    case TypeCode.Int32:
                        return value.GetInt32();

                    case TypeCode.UInt32:
                        return value.GetUInt32();

                    case TypeCode.Int64:
                        return value.GetInt64();

                    case TypeCode.UInt64:
                        return value.GetUInt64();

                    case TypeCode.Single:
                        return value.GetSingle();

                    case TypeCode.Double:
                        return value.GetDouble();

                    case TypeCode.Decimal:
                        return value.GetDecimal();

                    case TypeCode.DateTime:
                        return value.GetDateTime();

                    case TypeCode.String:
                        return value.GetString();

                    case TypeCode.Object:
                        {
                            if (type.IsGuidtype())
                            {
                                return value.GetGuid();
                            }

                            if (type.IsDictionaryType())
                            {
                                return _dataTransfers.ConvertJsonToDictionary(value.GetRawText(), type);
                            }

                            return _dataTransfers.ConvertJsonToObject(value.GetRawText(), type);
                        }

                    default:
                        return _dataTransfers.ConvertJsonToObject(value.GetRawText(), type);
                }
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, exception.Message);
            }

            return type.DefaultValue();
        }
    }
}
