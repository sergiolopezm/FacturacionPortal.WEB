using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System.Diagnostics.CodeAnalysis;

namespace FacturacionPortal.WEB.Shared.Forms
{
    /// <summary>
    /// Componente de entrada para valores Guid
    /// </summary>
    public class InputGuid : InputBase<Guid>
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
        /// Atributos adicionales para el elemento HTML
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

        /// <inheritdoc />
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "input");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "type", "text");
            builder.AddAttribute(3, "class", CssClass);
            builder.AddAttribute(4, "value", BindConverter.FormatValue(CurrentValueAsString));
            builder.AddAttribute(5, "onchange", EventCallback.Factory.CreateBinder<string?>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));

            if (!string.IsNullOrEmpty(Placeholder))
                builder.AddAttribute(6, "placeholder", Placeholder);

            if (ReadOnly)
                builder.AddAttribute(7, "readonly", ReadOnly);

            builder.CloseElement();
        }

        /// <inheritdoc />
        protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out Guid result, [NotNullWhen(false)] out string? validationErrorMessage)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result = default;
                validationErrorMessage = null;
                return true;
            }

            if (Guid.TryParse(value, out result))
            {
                validationErrorMessage = null;
                return true;
            }

            result = default;
            validationErrorMessage = $"El valor '{value}' no es un identificador válido.";
            return false;
        }

        /// <inheritdoc />
        protected override string? FormatValueAsString(Guid value)
        {
            return value == Guid.Empty ? string.Empty : value.ToString();
        }
    }

    /// <summary>
    /// Componente de entrada para valores Guid nullable
    /// </summary>
    public class InputNullableGuid : InputBase<Guid?>
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
        /// Atributos adicionales para el elemento HTML
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

        /// <inheritdoc />
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "input");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "type", "text");
            builder.AddAttribute(3, "class", CssClass);
            builder.AddAttribute(4, "value", BindConverter.FormatValue(CurrentValueAsString));
            builder.AddAttribute(5, "onchange", EventCallback.Factory.CreateBinder<string?>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));

            if (!string.IsNullOrEmpty(Placeholder))
                builder.AddAttribute(6, "placeholder", Placeholder);

            if (ReadOnly)
                builder.AddAttribute(7, "readonly", ReadOnly);

            builder.CloseElement();
        }

        /// <inheritdoc />
        protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out Guid? result, [NotNullWhen(false)] out string? validationErrorMessage)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result = null;
                validationErrorMessage = null;
                return true;
            }

            if (Guid.TryParse(value, out var guid))
            {
                result = guid;
                validationErrorMessage = null;
                return true;
            }

            result = null;
            validationErrorMessage = $"El valor '{value}' no es un identificador válido.";
            return false;
        }

        /// <inheritdoc />
        protected override string? FormatValueAsString(Guid? value)
        {
            return value?.ToString() ?? string.Empty;
        }
    }
}
