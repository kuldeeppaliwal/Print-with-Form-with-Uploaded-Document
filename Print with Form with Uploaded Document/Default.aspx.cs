using System;
using System.IO;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GeneratePdf(object sender, EventArgs e)
    {
        // Step 1: Capture Form Data
        string name = txtName.Text;
        string email = txtEmail.Text;

        // Step 2: Prepare PDF Document
        Document pdfDoc = new Document(PageSize.A4);
        MemoryStream memoryStream = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
        pdfDoc.Open();

        // Step 3: Add Form Data to PDF
        pdfDoc.Add(new Paragraph("Form Data:"));
        pdfDoc.Add(new Paragraph($"Name: {name}"));
        pdfDoc.Add(new Paragraph($"Email: {email}"));
        pdfDoc.Add(new Paragraph("\n\n"));  // Add some space

        // Step 4: Add Uploaded Document (if it's an image)
        if (fileUpload.HasFile)
        {
            string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();
            if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
            {
                // Convert the uploaded file to an iTextSharp Image
                iTextSharp.text.Image uploadedImage = iTextSharp.text.Image.GetInstance(fileUpload.FileContent);
                uploadedImage.ScaleToFit(500f, 500f); // Scale to fit in PDF
                pdfDoc.Add(uploadedImage);
            }
            else
            {
                pdfDoc.Add(new Paragraph("Uploaded document is not an image."));
            }
        }
        else
        {
            pdfDoc.Add(new Paragraph("No document was uploaded."));
        }

        // Step 5: Finalize and Output the PDF
        pdfDoc.Close();
        writer.Close();

        // Set up the response to download the PDF file
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=FormDataAndDocument.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.BinaryWrite(memoryStream.ToArray());
        Response.End();
    }
}