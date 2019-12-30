// using System.Text;
// using DependencyServices.Abstractions;
// using DependencyServices.WeatherModels;
//
// namespace DependencyServices
// {
//     public class HtmlWeatherDisplayService : IWeatherDisplayService 
//     {
//         public string GenerateDisplay(WeatherResult weatherResult)
//         {
//             var message = new StringBuilder();
//             message.AppendLine("Weather for today:<br/>");
//             message.AppendLine($"<b>High</b>: {weatherResult.main.temp_max}<br/>");
//             message.AppendLine($"<b>Low</b>: {weatherResult.main.temp_min}<br/>");
//             message.AppendLine($"<b>Current</b>: {weatherResult.main.temp}<br/>");
//             message.AppendLine("<br/>");
//             message.Append("You should wear ");
//             
//             if (weatherResult.main.temp < 40)
//             {
//                 message.Append("<i>a jacket</i>");
//             }
//             else if (weatherResult.main.temp < 60)
//             {
//                 message.Append("<i>a sweatshirt</i>");
//             }
//             else
//             {
//                 message.Append("<i>a t-shirt</i>");    
//             }
//             
//             
//
//             return message.ToString();
//         }
//     }
// }