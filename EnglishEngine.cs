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
    public class EnglishEngine
    {
        public static string Render()
        {
            TemplateContext context = new TemplateContext { FileProvider = new PhysicalFileProvider(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? Environment.CurrentDirectory, "views")) };

            return !EnglishTemplate.TryParse("{% include 'file' %}", out EnglishTemplate template) ? string.Empty : template.Render(context);
        }
        public static string RenderGlobal()
        {
            TemplateContext context = new TemplateContext { FileProvider = new PhysicalFileProvider(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? Environment.CurrentDirectory, "views")) };

            return !EnglishTemplateGlobal.TryParse("{% include 'file' %}", out EnglishTemplateGlobal template) ? string.Empty : template.Render(context);
        }
    }

    internal class EnglishTemplateGlobal : BaseFluidTemplate<EnglishTemplateGlobal>
    {
        static EnglishTemplateGlobal()
        {
            // This works but messes up the Global Context

            FluidTemplate.Factory.RegisterTag<EnglishTextTag>("text");
        }
    }

    internal class EnglishTemplate : BaseFluidTemplate<EnglishTemplate>
    {
        static EnglishTemplate()
        {
            // This doesn't work

            BaseFluidTemplate<EnglishTemplate>.Factory.RegisterTag<EnglishTextTag>("text");
        }
    }

    internal class EnglishTextTag : SimpleTag
    {
        public override async Task<Completion> WriteToAsync(TextWriter writer, TextEncoder encoder, TemplateContext context)
        {
            TextStatement statement = new TextStatement("Hello!");

            await statement.WriteToAsync(writer, encoder, context);

            return Completion.Normal;
        }
    }
}
