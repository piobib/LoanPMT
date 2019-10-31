using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoanAmortization.Pages.Result
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string pv { get; set; }
        [BindProperty(SupportsGet = true)]
        public string rate { get; set; }
        [BindProperty(SupportsGet = true)]
        public string quantity { get; set; }
        [BindProperty(SupportsGet = true)]
        public int pmtType { get; set; }
        [BindProperty(SupportsGet = true)]
        public string currency { get; set; }

        public void OnGet()
        {
            string message = "";

            if (!string.IsNullOrEmpty(pv) && !string.IsNullOrEmpty(rate) && !string.IsNullOrEmpty(quantity))
            {
                pv = pv.Replace('.', ',');
                rate = rate.Replace(".", ",");

                int error = 0;

                double parseRate, parsePv;
                int parseQuantity;


                if (!double.TryParse(pv, out parsePv))
                {
                    message = "Kwota kredytu musi być liczbą!";
                    error = 1;
                }
                else if (parsePv <= 0)
                {
                    message = $"{message} Kwota kredytu musi byc większa od zera!";
                    error = 1;
                }

                if (!double.TryParse(rate, out parseRate))
                {
                    if (error == 1)
                    {
                        message = $"{message} <br /><br />";
                    }
                    message = $"{message} Oprocentowanie kredytu musi być liczbą!";
                    error = 1;
                }
                else if (parseRate <= 0)
                {
                    if (error == 1)
                    {
                        message = $"{message} <br /><br />";
                    }
                    message = $"{message} Oprocentowanie kredytu musi byc większa od zera!";
                    error = 1;
                }

                if (!int.TryParse(quantity, out parseQuantity))
                {
                    if (error == 1)
                    {
                        message = $"{message} <br /><br />";
                    }
                    message = $"{message} Liczba rat kredytu musi być liczbą całkowitą!";
                    error = 1;
                }
                else if (parseQuantity <= 0)
                {
                    if (error == 1)
                    {
                        message = $"{message} <br /><br />";
                    }
                    message = $"{message} Liczba rat kredytu musi byc większa od zera!";
                    error = 1;
                }
                if (pmtType != 1 && pmtType != 2)
                {
                    if (error == 1)
                    {
                        message = $"{message} <br /><br />";
                    }
                    message = $"{message} Błędny typ raty!";
                    error = 1;
                }
                if (currency != "PLN" && currency != "CHF" && currency != "EUR" && currency != "USD")
                {
                    if (error == 1)
                    {
                        message = $"{message} <br /><br />";
                    }
                    message = $"{message} Proszę wybrać poprawną walutę kredytu!";
                    error = 1;
                }
                if (error == 0)
                {
                    double pmt = parsePv / ((1 - (1 / Math.Pow(1 + (parseRate / 100 / 12), parseQuantity))) / ((1 + parseRate / 100 / 12) - 1));

                    if (pmtType == 2)
                    {
                        pmt = pmt / (1 + parseRate / 100 / 12);
                    }
                    ViewData["pmt"] = Math.Round(pmt, 2);
                    ViewData["pv"] = parsePv;
                    ViewData["rate"] = parseRate;
                    ViewData["quantity"] = parseQuantity;
                }
                ViewData["message"] = $"{message}";


            }
            else
            {
                message = "Proszę wprowadzić wszystkie dane wymagane w formularzu!";

            }
            ViewData["message"] = message;
        }
    }
}