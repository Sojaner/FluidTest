using System;
using System.IO;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Fluid;
using Fluid.Ast;
using Fluid.Tags;
using Microsoft.Extensions.FileProviders;

namespace FluidTest
{
    public class SwedishEngine
    {
        public static string Render()
        {
            TemplateContext context = new TemplateContext { FileProvider = new PhysicalFileProvider(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? Environment.CurrentDirectory, "views")) };

            return !SwedishTemplate.TryParse("{% include 'file' %}", out SwedishTemplate template) ? string.Empty : template.Render(context);
        }
        public static string RenderGlobal()
        {
            TemplateContext context = new TemplateContext { FileProvider = new PhysicalFileProvider(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? Environment.CurrentDirectory, "views")) };

            return !SwedishTemplateGlobal.TryParse("{% include 'file' %}", out SwedishTemplateGlobal template) ? string.Empty : template.Render(context);
        }
    }

    internal class SwedishTemplateGlobal : BaseFluidTemplate<SwedishTemplateGlobal>
    {
        static SwedishTemplateGlobal()
        {
            // This works but messes up the Global Context

            FluidTemplate.Factory.RegisterTag<SwedishTextTag>("text");
        }
    }

    internal class SwedishTemplate : BaseFluidTemplate<SwedishTemplate>
    {
        static SwedishTemplate()
        {
            // This doesn't work

            BaseFluidTemplate<SwedishTemplate>.Factory.RegisterTag<SwedishTextTag>("text");
        }
    }

    internal class SwedishTextTag : SimpleTag
    {
        public override async Task<Completion> WriteToAsync(TextWriter writer, TextEncoder encoder, TemplateContext context)
        {
            TextStatement statement = new TextStatement("Hej!");

            await statement.WriteToAsync(writer, encoder, context);

            return Completion.Normal;
        }
    }
}
