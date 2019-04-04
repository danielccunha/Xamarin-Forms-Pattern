using MyProject.Utility.Converters;
using System;
using System.Linq;
using Xamarin.Forms;

namespace MyProject.Behaviors
{
    public class TextToNumberBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty OptionsProperty =
            BindableProperty.Create(nameof(Options), typeof(NumberOptions), typeof(TextToNumberBehavior), NumberOptions.None);

        public NumberOptions Options
        {
            get => (NumberOptions)GetValue(OptionsProperty);
            set => SetValue(OptionsProperty, value);
        }

        public static readonly BindableProperty DefaultValueProperty =
            BindableProperty.Create(nameof(DefaultValue), typeof(decimal), typeof(TextToNumberBehavior), 0m);

        public decimal DefaultValue
        {
            get => (decimal)GetValue(DefaultValueProperty);
            set => SetValue(DefaultValueProperty, value);
        }

        public static readonly BindableProperty UseDefaultValueProperty =
            BindableProperty.Create(nameof(UseDefaultValue), typeof(bool), typeof(TextToNumberBehavior), true);

        public bool UseDefaultValue
        {
            get => (bool)GetValue(UseDefaultValueProperty);
            set => SetValue(UseDefaultValueProperty, value);
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEntryTextChanged;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Options.HasFlag(NumberOptions.None))
                return;

            if (e.NewTextValue.Length == 1 && (e.NewTextValue[0] == '-' || e.NewTextValue[0] == '.' || e.NewTextValue[0] == ','))
                return;

            var entry = sender as Entry;
            var textValue = e.NewTextValue.Replace(',', '.');

            if (string.IsNullOrWhiteSpace(textValue))
            {
                if (UseDefaultValue)
                    entry.Text = DefaultValue.ToString();

                return;
            }
            else if (NumberConverter.TryParseDecimal(textValue, out decimal value))
            {
                if (Options.HasFlag(NumberOptions.Integer))
                    value = Math.Floor(value);

                if (Options.HasFlag(NumberOptions.Positive))
                    value = Math.Abs(value);

                var valueStr = value.ToString();

                if (e.NewTextValue.LastOrDefault() is char lastChar && !char.IsNumber(lastChar))
                    valueStr += lastChar;

                entry.Text = valueStr;
            }
            else if (entry.Keyboard == Keyboard.Numeric)
                throw new ArgumentException($"Não foi possível converter a string para um número, possivelmente porque não possui apenas números: {e.NewTextValue}.");
        }

        [Flags]
        public enum NumberOptions
        {
            None = 0x01,
            Integer = 0x02,
            Positive = 0x04
        }
    }
}
