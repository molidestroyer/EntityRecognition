using CognitivePlayground.Shared;
using Microsoft.AspNetCore.Blazor.Components;

namespace CognitivePlayground.Client.Shared.Components
{
    public class CardEntityComponentModel : BlazorComponent

    {
        [Parameter]
        protected EntityRecord EntityRecord { get; set; }

        protected string Color
        {
            get
            {
                switch (EntityRecord.Type)
                {
                    case "Person":
                        return "rgba(187, 120, 36, 0.1)";
                    case "Location":
                        return "rgba(22, 160, 133, 0.1)";
                    case "Organization":
                        return "rgba(213, 15, 37, 0.1)";
                    case "Quantity":
                        return "rgba(51, 105, 232, 0.1)";
                    case "DateTime":
                        return "rgba(250, 188, 9, 0.1)";
                    case "URL":
                        return "rgba(121, 90, 71, 0.1)";
                    case "Email":
                        return "rgba(130, 93, 9, 0.1)";
                }
                return "rgba(130, 93, 9, 0.1)"; ;
            }
        }

        protected string IconColor
        {
            get
            {
                switch (EntityRecord.Type)
                {
                    case "Person":
                        return "#BB7824";
                    case "Location":
                        return "#16A085";
                    case "Organization":
                        return "#d50f25";
                    case "Quantity":
                        return "#3369e8";
                    case "DateTime":
                        return "#fabc09";
                    case "URL":
                        return "#795a47";
                    case "Email":
                        return "#825d09";
                }
                return "#825d09";
            }
        }
        
        protected string Icon
        {
            get
            {
                switch (EntityRecord.Type)
                {
                    case "Person":
                        return "fa fa-user";
                    case "Location":
                        return "fa fa-map-pin";
                    case "Organization":
                        return "fa fa-sitemap";
                    case "Quantity":
                        return "fa fa-sort-numeric-desc ";
                    case "DateTime":
                        return "fa fa-calendar";
                    case "URL":
                        return "fa fa-link";
                    case "Email":
                        return "fa fa-envelope-o";
                }
                return "fa fa-wikipedia-w";
            }
        }
    }
}