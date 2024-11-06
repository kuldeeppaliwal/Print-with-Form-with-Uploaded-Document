using System;
using System.IO;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfSharp.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

public partial class Default_for_PDF_Document : System.Web.UI.Page
{
    protected void GeneratePdf(object sender, EventArgs e)
    {
        string htmlContent = GetHtmlContent(); // Method to get the HTML content
        MemoryStream outputStream = new MemoryStream();

        try
        {
            // Step 1: Convert HTML Content to PDF
            PdfSharp.Pdf.PdfDocument htmlPdf = ConvertHtmlToPdf(htmlContent);

            // Step 2: Create Final PDF Document
            Document finalPdfDoc = new Document(PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(finalPdfDoc, outputStream);
            finalPdfDoc.Open();

            // Step 3: Merge HTML PDF Content
            using (MemoryStream htmlPdfStream = new MemoryStream())
            {
                htmlPdf.Save(htmlPdfStream, false);
                htmlPdfStream.Position = 0;

                PdfReader htmlPdfReader = new PdfReader(htmlPdfStream);
                AppendPdfContent(writer, finalPdfDoc, htmlPdfReader);                
            }

            // Step 4: Append Uploaded PDF Content (if exists)
            if (fileUpload.HasFile && Path.GetExtension(fileUpload.FileName).ToLower() == ".pdf")
            {
                using (MemoryStream uploadedPdfStream = new MemoryStream())
                {
                    fileUpload.FileContent.CopyTo(uploadedPdfStream);
                    uploadedPdfStream.Position = 0;

                    PdfReader uploadedPdfReader = new PdfReader(uploadedPdfStream);
                    AppendPdfContent(writer, finalPdfDoc, uploadedPdfReader);                    
                }
            }

            finalPdfDoc.Close();
            writer.Close();

            // Step 5: Output the PDF as downloadable
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=CombinedDocument.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(outputStream.ToArray());
            Response.End();
        }
        catch (Exception ex)
        {
            Response.Write("Error generating PDF: " + ex.Message);
        }
        finally
        {
            outputStream.Dispose();
        }
    }

    private PdfSharp.Pdf.PdfDocument ConvertHtmlToPdf(string htmlContent)
    {
        // Convert HTML string to PDF using HtmlRenderer.PdfSharp
        PdfSharp.Pdf.PdfDocument pdf = PdfGenerator.GeneratePdf(htmlContent, PdfSharp.PageSize.A4);
        return pdf;
    }

    private void AppendPdfContent(PdfWriter writer, Document finalPdfDoc, PdfReader pdfReader)
    {
        // Appends each page from a PdfReader to the final document
        for (int i = 1; i <= pdfReader.NumberOfPages; i++)
        {
            finalPdfDoc.NewPage();
            PdfImportedPage page = writer.GetImportedPage(pdfReader, i);
            writer.DirectContent.AddTemplate(page, 0, 0);
        }
    }

    //private string GetHtmlContent()
    //{
    //    // Example HTML content; replace with actual HTML generation logic
    //    return @"
    //        <html>
    //        <body>
    //            <h1>Form Data</h1>
    //            <p>Name: John Doe</p>
    //            <p>Email: john.doe@example.com</p>
    //            <p>This is the content of the HTML page that will be added to the PDF.</p>
    //        </body>
    //        </html>";
    //}

    private string GetHtmlContent()
    {
        // Create dynamic HTML content using the provided form values
        string htmlContent = $@"
                <html>
                <body>
                    <h1>Registration Details</h1>
                    <p><strong>Name:</strong> {txtname.Text}</p>
                    <p><strong>Email:</strong> {txtemail.Text}</p>                    
                </body>
                </html>";

        return htmlContent;
    }
}
