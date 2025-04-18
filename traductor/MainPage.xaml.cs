using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace traductor
{
    public partial class MainPage : ContentPage
    {
        private readonly Dictionary<string, string> morseToText = new()
        {
            { ".-", "A" }, { "-...", "B" }, { "-.-.", "C" }, { "-..", "D" },
            { ".", "E" }, { "..-.", "F" }, { "--.", "G" }, { "....", "H" },
            { "..", "I" }, { ".---", "J" }, { "-.-", "K" }, { ".-..", "L" },
            { "--", "M" }, { "-.", "N" }, { "---", "O" }, { ".--.", "P" },
            { "--.-", "Q" }, { ".-.", "R" }, { "...", "S" }, { "-", "T" },
            { "..-", "U" }, { "...-", "V" }, { ".--", "W" }, { "-..-", "X" },
            { "-.--", "Y" }, { "--..", "Z" }, { "-----", "0" }, { ".----", "1" },
            { "..---", "2" }, { "...--", "3" }, { "....-", "4" }, { ".....", "5" },
            { "-....", "6" }, { "--...", "7" }, { "---..", "8" }, { "----.", "9" }
        };

        private readonly Dictionary<string, string> textToMorse;

        public MainPage()
        {
            InitializeComponent();
            textToMorse = morseToText.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
        }

        private void OnTraducirClicked(object sender, EventArgs e)
        {
            var entrada = EntradaTexto.Text?.Trim();
            if (string.IsNullOrEmpty(entrada)) return;

            string resultado = "";

            if (MorseANaturalRadio.IsChecked)
            {
                resultado = TraducirMorseATexto(entrada);
            }
            else if (NaturalAMorseRadio.IsChecked)
            {
                resultado = TraducirTextoAMorse(entrada);
            }

            ResultadoTexto.Text = resultado;
        }

        private void OnLimpiarClicked(object sender, EventArgs e)
        {
            EntradaTexto.Text = string.Empty;
            ResultadoTexto.Text = string.Empty;
            MorseANaturalRadio.IsChecked = false;
            NaturalAMorseRadio.IsChecked = false;
        }

        private string TraducirMorseATexto(string morse)
        {
            var palabras = morse.Split("   ");
            return string.Join(" ", palabras.Select(palabra =>
                string.Join("", palabra.Split(' ').Select(simbolo => morseToText.GetValueOrDefault(simbolo, "")))));
        }

        private string TraducirTextoAMorse(string texto)
        {
            return string.Join(" ", texto.ToUpper().Where(c => c != ' ').Select(c => textToMorse.GetValueOrDefault(c.ToString(), "")));
        }
    }
}
