using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace FacturacionPortal.WEB.Shared.Forms
{
    /// <summary>
    /// Componente de entrada para valores decimales
    /// </summary>
    public class InputDecimal : InputBase<decimal>
    {
        /// <summary>
        /// Placeholder del input
        /// </summary>
        [Parameter] public string? Placeholder { get; set; }

        /// <summary>
        /// Indica si el campo es de solo lectura
        /// </summary>
        [Parameter] public bool ReadOnly { get; set; }

        /// <summary>
        /// Valor mínimo permitido
        /// </summary>
        [Parameter] public decimal? Min { get; set; }

        /// <summary>
        /// Valor máximo permitido
        /// </summary>
        [Parameter] public decimal? Max { get; set; }

        /// <summary>
        /// Incremento para los controles de número
        /// </summary>
        [Parameter] public decimal Step { get; set; } = 0.01m;

        /// <summary>
        /// Número de decimales a mostrar
        /// </summary>
        [Parameter] public int DecimalPlaces { get; set; } = 2;

        /// <summary>
        /// Formato de cultura a usar
        /// </summary>
        [Parameter] public CultureInfo? Culture { get; set; }

        /// <summary>
        /// Atributos adicionales para el elemento HTML
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

        private CultureInfo EffectiveCulture => Culture ?? new CultureInfo("es-CO");

        /// <inheritdoc />
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "input");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "type", "number");
            builder.AddAttribute(3, "class", CssClass);
            builder.AddAttribute(4, "value", BindConverter.FormatValue(CurrentValueAsString));
            builder.AddAttribute(5, "onchange", EventCallback.Factory.CreateBinder<string?>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));

            if (!string.IsNullOrEmpty(Placeholder))
                builder.AddAttribute(6, "placeholder", Placeholder);

            if (ReadOnly)
                builder.AddAttribute(7, "readonly", ReadOnly);

            if (Min.HasValue)
                builder.AddAttribute(8, "min", Min.Value.ToString(EffectiveCulture));

            if (Max.HasValue)
                builder.AddAttribute(9, "max", Max.Value.ToString(EffectiveCulture));

            builder.AddAttribute(10, "step", Step.ToString(EffectiveCulture));

            builder.CloseElement();
        }

        /// <inheritdoc />
        protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out decimal result, [NotNullWhen(false)] out string? validationErrorMessage)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result = default;
                validationErrorMessage = null;
                return true;
            }

            if (decimal.TryParse(value, NumberStyles.Number, EffectiveCulture, out result))
            {
                // Validar rango
                if (Min.HasValue && result < Min.Value)
                {
                    validationErrorMessage = $"El valor debe ser mayor o igual a {Min.Value.ToString($"N{DecimalPlaces}", EffectiveCulture)}";
                    return false;
                }

                if (Max.HasValue && result > Max.Value)
                {
                    validationErrorMessage = $"El valor debe ser menor o igual a {Max.Value.ToString($"N{DecimalPlaces}", EffectiveCulture)}";
                    return false;
                }

                validationErrorMessage = null;
                return true;
            }

            result = default;
            validationErrorMessage = $"El valor '{value}' no es un número válido.";
            return false;
        }

        /// <inheritdoc />
        protected override string? FormatValueAsString(decimal value)
        {
            return value.ToString($"F{DecimalPlaces}", EffectiveCulture);
        }
    }

    /// <summary>
    /// Componente de entrada para valores decimales nullable
    /// </summary>
    public class InputNullableDecimal : InputBase<decimal?>
    {
        /// <summary>
        /// Placeholder del input
        /// </summary>
        [Parameter] public string? Placeholder { get; set; }

        /// <summary>
        /// Indica si el campo es de solo lectura
        /// </summary>
        [Parameter] public bool ReadOnly { get; set; }

        /// <summary>
        /// Valor mínimo permitido
        /// </summary>
        [Parameter] public decimal? Min { get; set; }

        /// <summary>
        /// Valor máximo permitido
        /// </summary>
        [Parameter] public decimal? Max { get; set; }

        /// <summary>
        /// Incremento para los controles de número
        /// </summary>
        [Parameter] public decimal Step { get; set; } = 0.01m;

        /// <summary>
        /// Número de decimales a mostrar
        /// </summary>
        [Parameter] public int DecimalPlaces { get; set; } = 2;

        /// <summary>
        /// Formato de cultura a usar
        /// </summary>
        [Parameter] public CultureInfo? Culture { get; set; }

        /// <summary>
        /// Atributos adicionales para el elemento HTML
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

        private CultureInfo EffectiveCulture => Culture ?? new CultureInfo("es-CO");

        /// <inheritdoc />
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "input");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "type", "number");
            builder.AddAttribute(3, "class", CssClass);
            builder.AddAttribute(4, "value", BindConverter.FormatValue(CurrentValueAsString));
            builder.AddAttribute(5, "onchange", EventCallback.Factory.CreateBinder<string?>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));

            if (!string.IsNullOrEmpty(Placeholder))
                builder.AddAttribute(6, "placeholder", Placeholder);

            if (ReadOnly)
                builder.AddAttribute(7, "readonly", ReadOnly);

            if (Min.HasValue)
                builder.AddAttribute(8, "min", Min.Value.ToString(EffectiveCulture));

            if (Max.HasValue)
                builder.AddAttribute(9, "max", Max.Value.ToString(EffectiveCulture));

            builder.AddAttribute(10, "step", Step.ToString(EffectiveCulture));

            builder.CloseElement();
        }

        /// <inheritdoc />
        protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out decimal? result, [NotNullWhen(false)] out string? validationErrorMessage)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result = null;
                validationErrorMessage = null;
                return true;
            }

            if (decimal.TryParse(value, NumberStyles.Number, EffectiveCulture, out var decimalValue))
            {
                // Validar rango
                if (Min.HasValue && decimalValue < Min.Value)
                {
                    result = null;
                    validationErrorMessage = $"El valor debe ser mayor o igual a {Min.Value.ToString($"N{DecimalPlaces}", EffectiveCulture)}";
                    return false;
                }

                if (Max.HasValue && decimalValue > Max.Value)
                {
                    result = null;
                    validationErrorMessage = $"El valor debe ser menor o igual a {Max.Value.ToString($"N{DecimalPlaces}", EffectiveCulture)}";
                    return false;
                }

                result = decimalValue;
                validationErrorMessage = null;
                return true;
            }

            result = null;
            validationErrorMessage = $"El valor '{value}' no es un número válido.";
            return false;
        }

        /// <inheritdoc />
        protected override string? FormatValueAsString(decimal? value)
        {
            return value?.ToString($"F{DecimalPlaces}", EffectiveCulture) ?? string.Empty;
        }
    }
}
