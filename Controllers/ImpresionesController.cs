using iTextSharp.text;
using iTextSharp.text.pdf;
using ManttoMVCCore.Data;
using ManttoMVCCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ManttoMVCCore.Controllers
{
    [Authorize]
    public class ImpresionesController : Controller
    {
        float[] widthsTitulosGenerales = new float[] { 1f };
        private DBOperaciones repo;

        public ImpresionesController()
        {
            repo = new DBOperaciones();
        }

        [Authorize(Roles = "Administrador, Operativo")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult printCatalogoArea(int p_opcion)
        {
            var datosCatalogo = repo.GetReportes<ImpresionViewModel>("sp_obtener_reportes", p_opcion).ToList();

            string mensajeTitulo = string.Empty;
            string cabecera = string.Empty;

            switch (p_opcion)
            {
                case 1:
                    mensajeTitulo = "CATALOGO DE AREAS";
                    cabecera = "Area";
                    break;
                case 2:
                    mensajeTitulo = "CATALOGO DE EQUIPOS";
                    cabecera = "Patrimonio";
                    break;
                case 3:
                    mensajeTitulo = "CATALOGO DE MARCAS";
                    cabecera = "Marca";
                    break;
                case 4:
                    mensajeTitulo = "CATALOGO DE RESGUARDANTES";
                    cabecera = "Resguardante";
                    break;
                case 5:
                    mensajeTitulo = "CATALOGO DE TESTIGOS DE ENTREGA";
                    cabecera = "Tipo de Equipo";
                    break;
                case 6:
                    mensajeTitulo = "CATALOGOS DE TIPO DE EQUIPO";
                    cabecera = "Tipo Equipo";
                    break;
            }

            MemoryStream msRep = new MemoryStream();
            Document docRep = new Document(PageSize.LETTER, 30f, 20f, 50f, 40f);
            PdfWriter pwRep = PdfWriter.GetInstance(docRep, msRep);

            pwRep.PageEvent = new HeaderReporte();

            docRep.Open();

            //string imagenPath = @"C:/inetpub/wwwroot/fotoUser/gobedohor.png";
            //Image imagen = Image.GetInstance(imagenPath);
            //imagen.ScalePercent(25f);
            //imagen.SetAbsolutePosition(30f, 725f);
            //docRep.Add(imagen);

            //Chunk chunkArea = new Chunk();

            #region Titulo
            Paragraph titulo = new Paragraph();
            titulo.Alignment = Element.ALIGN_CENTER;
            titulo.Font = FontFactory.GetFont("Arial", 10, Font.BOLDITALIC);
            titulo.Add("Detalle");
            titulo.Add(Chunk.NEWLINE);
            docRep.Add(titulo);
            #endregion

            #region TituloReporte
            PdfPTable tableTitulo = new PdfPTable(1);
            tableTitulo.TotalWidth = 560f;
            tableTitulo.LockedWidth = true;

            float[] widthsEstudio = new float[] { 1f };
            tableTitulo.SetWidths(widthsEstudio);
            tableTitulo.HorizontalAlignment = 0;
            tableTitulo.SpacingBefore = 10f;
            tableTitulo.SpacingAfter = 10f;

            PdfPCell cellTituloTitulo = new PdfPCell(new Phrase(mensajeTitulo, new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTitulo.HorizontalAlignment = 1;
            cellTituloTitulo.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTitulo.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTitulo.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTitulo.AddCell(cellTituloTitulo);

            docRep.Add(tableTitulo);
            #endregion

            #region DetalleArea
            Paragraph cabeceras = new Paragraph();
            if (p_opcion == 1 || p_opcion == 3 || p_opcion == 4 || p_opcion == 6)
            {
                cabeceras.Alignment = Element.ALIGN_LEFT;
                cabeceras.Font = FontFactory.GetFont("Arial", 10, Font.BOLDITALIC);
                cabeceras.Add(Chunk.TABBING); cabeceras.Add("#");
                cabeceras.Add(Chunk.TABBING); cabeceras.Add(Chunk.TABBING); cabeceras.Add(Chunk.TABBING); cabeceras.Add(Chunk.TABBING); cabeceras.Add(Chunk.TABBING);
                cabeceras.Add(cabecera);
                cabeceras.Add(Chunk.NEWLINE);
                cabeceras.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL); cabeceras.Add(Chunk.NEWLINE);
            }
            else if (p_opcion == 2)
            {
                cabeceras.Alignment = Element.ALIGN_LEFT;
                cabeceras.Font = FontFactory.GetFont("Arial", 10, Font.BOLDITALIC);
                cabeceras.Add("#");
                cabeceras.Add(Chunk.TABBING);
                cabeceras.Add(cabecera);
                cabeceras.Add(Chunk.TABBING); cabeceras.Add(Chunk.TABBING);
                cabeceras.Add("Clave");
                cabeceras.Add(Chunk.TABBING);
                cabeceras.Add("Descripcion");
                cabeceras.Add(Chunk.TABBING); cabeceras.Add(Chunk.TABBING); cabeceras.Add(Chunk.TABBING); cabeceras.Add(Chunk.TABBING);
                cabeceras.Add("Serie");
                cabeceras.Add(Chunk.TABBING);
                cabeceras.Add("Activo");
                cabeceras.Add(Chunk.NEWLINE);
                cabeceras.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL); cabeceras.Add(Chunk.NEWLINE);
            }

            //Contenido de las areas
            foreach (var item in datosCatalogo)
            {
                if (p_opcion == 1 || p_opcion == 3 || p_opcion == 4 || p_opcion == 5 || p_opcion == 6)
                {
                    cabeceras.Add(Chunk.TABBING); cabeceras.Add(item.id.ToString());
                    cabeceras.Add(Chunk.TABBING); cabeceras.Add(Chunk.TABBING); cabeceras.Add(Chunk.TABBING); cabeceras.Add(Chunk.TABBING); cabeceras.Add(Chunk.TABBING);
                    cabeceras.Add(item.valor.ToString());
                    cabeceras.Add(Chunk.NEWLINE);
                }
                else if (p_opcion == 2)
                {
                    cabeceras.Add(item.id.ToString()); cabeceras.Add(Chunk.TABBING);
                    cabeceras.Add(item.valor.ToString()); cabeceras.Add(Chunk.TABBING);
                    cabeceras.Add(item.clave.ToString()); cabeceras.Add(Chunk.TABBING);
                    cabeceras.Add(item.descripcion.ToString()); cabeceras.Add(Chunk.TABBING);
                    cabeceras.Add(item.serie.ToString()); cabeceras.Add(Chunk.TABBING);
                    cabeceras.Add(item.activo.ToString()); cabeceras.Add(Chunk.TABBING);

                    cabeceras.Add(Chunk.NEWLINE);
                }
            }
            //Contenido de las areas

            docRep.Add(cabeceras);
            #endregion

            docRep.Close();

            byte[] bytesStream = msRep.ToArray();

            msRep = new MemoryStream();

            msRep.Write(bytesStream, 0, bytesStream.Length);

            msRep.Position = 0;

            return new FileStreamResult(msRep, "application/pdf");

        }

        public IActionResult printMantenimiento(int id)
        {
            var datosMan = repo.GetReportes<ImpresionViewModel>("sp_obtener_impresion_mantenimiento", id).FirstOrDefault();

            MemoryStream msRep = new MemoryStream();

            Document docRep = new Document(PageSize.LETTER, 30f, 20f, 70f, 40f);
            PdfWriter pwRep = PdfWriter.GetInstance(docRep, msRep);

            string recibe = datosMan.resguardante.ToString();
            string realizo = datosMan.realizo.ToString();
            string jefe = datosMan.jefe.ToString();

            pwRep.PageEvent = HeaderReporteResguardo.getMultilineFooter(recibe, realizo, jefe);

            docRep.Open();

            Paragraph derecha = new Paragraph();
            derecha.Alignment = Element.ALIGN_RIGHT;
            derecha.Font = FontFactory.GetFont("Arial", 11, Font.BOLDITALIC);
            derecha.Add("Folio: ");
            derecha.Font = FontFactory.GetFont("Arial", 15, Font.NORMAL + Font.UNDERLINE);
            derecha.Add(Chunk.TABBING);
            derecha.Add(datosMan.id.ToString());
            derecha.Add(Chunk.NEWLINE);
            derecha.Font = FontFactory.GetFont("Arial", 11, Font.BOLDITALIC);
            derecha.Add("Fecha: ");
            derecha.Font = FontFactory.GetFont("Arial", 11, Font.NORMAL);
            derecha.Add(Chunk.TABBING); derecha.Add(datosMan.fecha.ToString());
            derecha.Add(Chunk.NEWLINE); derecha.Add(Chunk.NEWLINE);

            docRep.Add(derecha);

            #region Titulo datos del resguardante
            PdfPTable tableTituloRegistral = new PdfPTable(1);
            tableTituloRegistral.TotalWidth = 560f;
            tableTituloRegistral.LockedWidth = true;

            float[] widthsEstudio = new float[] { 1f };
            tableTituloRegistral.SetWidths(widthsEstudio);
            tableTituloRegistral.HorizontalAlignment = 0;
            tableTituloRegistral.SpacingBefore = 10f;
            tableTituloRegistral.SpacingAfter = 10f;

            PdfPCell cellTituloTituloRegistral = new PdfPCell(new Phrase("DATOS DEL RESGUARDANTE", new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLDITALIC)));
            cellTituloTituloRegistral.HorizontalAlignment = 1;
            cellTituloTituloRegistral.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloRegistral.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloRegistral.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloRegistral.AddCell(cellTituloTituloRegistral);

            docRep.Add(tableTituloRegistral);
            #endregion

            Font tituloTabla = FontFactory.GetFont("Arial", 11, Font.BOLD);
            Font datoTabla = FontFactory.GetFont("Arial", 11, Font.NORMAL);

            #region tabla Datos del resguardante
            PdfPTable tblDetalleResguardante = new PdfPTable(2);
            float[] widthsDetalle = new float[] { 20f, 80f };
            tblDetalleResguardante.WidthPercentage = 100f;

            tblDetalleResguardante.SetWidths(widthsDetalle);
            tblDetalleResguardante.HorizontalAlignment = 0;
            tblDetalleResguardante.SpacingBefore = 5f;
            tblDetalleResguardante.SpacingAfter = 10f;

            tblDetalleResguardante.AddCell(new Phrase("Nombre", tituloTabla));
            tblDetalleResguardante.AddCell(new Phrase(datosMan.resguardante.ToString(), datoTabla));

            tblDetalleResguardante.AddCell(new Phrase("Area de Adscripcion", tituloTabla));
            tblDetalleResguardante.AddCell(new Phrase(datosMan.area.ToString(), datoTabla));

            tblDetalleResguardante.AddCell(new Phrase("Extension", tituloTabla));
            tblDetalleResguardante.AddCell(new Phrase(datosMan.extension.ToString(), datoTabla));

            docRep.Add(tblDetalleResguardante);
            #endregion

            #region Titulo datos del equipo
            PdfPTable tableTituloEquipo = new PdfPTable(1);
            tableTituloEquipo.TotalWidth = 560f;
            tableTituloEquipo.LockedWidth = true;

            float[] widthsEquipo = new float[] { 1f };
            tableTituloEquipo.SetWidths(widthsEstudio);
            tableTituloEquipo.HorizontalAlignment = 0;
            tableTituloEquipo.SpacingBefore = 10f;
            tableTituloEquipo.SpacingAfter = 10f;

            PdfPCell cellTituloTituloEquipo = new PdfPCell(new Phrase("DATOS DEL EQUIPO", new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLDITALIC)));
            cellTituloTituloEquipo.HorizontalAlignment = 1;
            cellTituloTituloEquipo.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloEquipo.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloEquipo.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloEquipo.AddCell(cellTituloTituloEquipo);

            docRep.Add(tableTituloEquipo);
            #endregion

            #region table Datos del Equipo
            PdfPTable tblDetEquipo = new PdfPTable(2);
            float[] widthsDetEquipo = new float[] { 30f, 70f };
            tblDetEquipo.WidthPercentage = 100f;
            tblDetEquipo.SetWidths(widthsDetEquipo);

            tblDetEquipo.HorizontalAlignment = 0;
            tblDetEquipo.SpacingBefore = 5f;
            tblDetEquipo.SpacingAfter = 30f;

            tblDetEquipo.AddCell(new Phrase("Descripcion", tituloTabla));
            tblDetEquipo.AddCell(new Phrase(datosMan.descripcion.ToString(), datoTabla));
            tblDetEquipo.AddCell(new Phrase("Numero de Serie", tituloTabla));
            tblDetEquipo.AddCell(new Phrase(datosMan.serie.ToString(), datoTabla));
            tblDetEquipo.AddCell(new Phrase("Numero de Patrimonio", tituloTabla));
            tblDetEquipo.AddCell(new Phrase(datosMan.inventario.ToString(), datoTabla));
            tblDetEquipo.AddCell(new Phrase("Accesorios entregados", tituloTabla));
            tblDetEquipo.AddCell(new Phrase(datosMan.accesorios.ToString(), datoTabla));
            tblDetEquipo.AddCell(new Phrase("Falla reportada", tituloTabla));
            tblDetEquipo.AddCell(new Phrase(datosMan.falla.ToString(), datoTabla));

            docRep.Add(tblDetEquipo);

            #endregion

            #region Firma recibe
            Paragraph firmaRecibe = new Paragraph();
            firmaRecibe.Alignment = Element.ALIGN_CENTER;
            firmaRecibe.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            firmaRecibe.Add("______________________________");
            firmaRecibe.Add(Chunk.NEWLINE);
            firmaRecibe.Add("Recibe: " + datosMan.recibe.ToString());
            firmaRecibe.Add(Chunk.NEWLINE); firmaRecibe.Add(Chunk.NEWLINE);

            docRep.Add(firmaRecibe);
            #endregion

            #region Diagnostico tecnico
            Paragraph diagnostico = new Paragraph();
            diagnostico.Alignment = Element.ALIGN_LEFT;
            diagnostico.Font = FontFactory.GetFont("Arial", 11, Font.BOLD);
            diagnostico.Add("Diagnostico tecnico");
            diagnostico.Add(Chunk.NEWLINE);

            docRep.Add(diagnostico);

            PdfPTable tblDiagnostico = new PdfPTable(1);
            float[] widthsDiagnostico = new float[] { 100f };
            tblDiagnostico.WidthPercentage = 100f;
            tblDiagnostico.SetWidths(widthsDiagnostico);

            tblDiagnostico.HorizontalAlignment = 0;
            tblDiagnostico.SpacingBefore = 5f;
            tblDiagnostico.SpacingAfter = 30f;

            tblDiagnostico.AddCell(new Phrase(datosMan.diagnostico.ToString(), datoTabla));

            docRep.Add(tblDiagnostico);
            #endregion

            #region Dictamen tecnico
            Paragraph dictamen = new Paragraph();
            dictamen.Alignment = Element.ALIGN_LEFT;
            dictamen.Font = FontFactory.GetFont("Arial", 11, Font.BOLD);
            dictamen.Add("Dictamen tecnico");
            dictamen.Add(Chunk.NEWLINE);

            docRep.Add(dictamen);

            PdfPTable tblDictamen = new PdfPTable(1);
            float[] widthsDictamen = new float[] { 100f };
            tblDictamen.WidthPercentage = 100f;
            tblDictamen.SetWidths(widthsDictamen);

            tblDictamen.HorizontalAlignment = 0;
            tblDictamen.SpacingBefore = 5f;
            tblDictamen.SpacingAfter = 30f;

            tblDictamen.AddCell(new Phrase(datosMan.dictamen.ToString(), datoTabla));

            docRep.Add(tblDictamen);
            #endregion

            Paragraph nota = new Paragraph();
            nota.Alignment = Element.ALIGN_JUSTIFIED;
            nota.Font = FontFactory.GetFont("Arial", 11, Font.BOLD);
            nota.Add("Nota: ");
            nota.Font = FontFactory.GetFont("Arial", 11, Font.NORMAL);
            nota.Add("En caso de realizar respaldo, despues de la entrega del equipo, este solo sera resguardado por un lapso de 15 dias, por el area de Soporte Tecnico, despues de ese tiempo el area no se hace responsable por dicha informacion.");
            nota.Add(Chunk.NEWLINE);

            docRep.Add(nota);

            docRep.Close();

            byte[] bytesStream = msRep.ToArray();

            msRep = new MemoryStream();

            msRep.Write(bytesStream, 0, bytesStream.Length);

            msRep.Position = 0;

            return new FileStreamResult(msRep, "application/pdf");
        }

    }

    public class HeaderReporte : PdfPageEventHelper
    {
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            base.OnOpenDocument(writer, document);
        }

        // write on start of each page
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            Rectangle page = document.PageSize;

            //===========
            string imageizq = @"C:/inetpub/wwwroot/fotoUser/gobedohor.png";
            iTextSharp.text.Image jpgSupIzq = iTextSharp.text.Image.GetInstance(imageizq);
            jpgSupIzq.ScaleToFit(80f, 80f);

            PdfPCell clLogoSupIzq = new PdfPCell();
            clLogoSupIzq.BorderWidth = 0;
            clLogoSupIzq.VerticalAlignment = Element.ALIGN_BOTTOM;
            clLogoSupIzq.AddElement(jpgSupIzq);
            //==========

            //
            string imageder = @"C:/inetpub/wwwroot/fotoUser/nuevoCeccc.png";
            iTextSharp.text.Image jpgSupDer = iTextSharp.text.Image.GetInstance(imageder);
            jpgSupDer.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
            jpgSupDer.ScaleToFit(100f, 100f);

            PdfPCell clLogoSupDer = new PdfPCell();
            clLogoSupDer.BorderWidth = 0;
            clLogoSupDer.VerticalAlignment = Element.ALIGN_BOTTOM;
            clLogoSupDer.AddElement(jpgSupDer);
            //==========

            Chunk chkTit = new Chunk("Unidad de Informática", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
            Paragraph paragraph = new Paragraph();
            paragraph.Alignment = Element.ALIGN_CENTER;
            //paragraph.SetLeading(10, 1);
            paragraph.Add(chkTit);

            Chunk chkSub = new Chunk("Area de Sistemas", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
            Paragraph paragraph1 = new Paragraph();
            paragraph1.Alignment = Element.ALIGN_CENTER;
            //paragraph.SetLeading(10, 1);
            paragraph1.Add(chkSub);

            PdfPCell clTitulo = new PdfPCell();
            clTitulo.BorderWidth = 0;
            clTitulo.AddElement(paragraph);

            PdfPCell clSubTit = new PdfPCell();
            clSubTit.BorderWidth = 0;
            clSubTit.AddElement(paragraph1);

            PdfPTable tblTitulo = new PdfPTable(1);
            tblTitulo.WidthPercentage = 100;
            tblTitulo.AddCell(clTitulo);
            tblTitulo.AddCell(clSubTit);

            PdfPCell clTablaTitulo = new PdfPCell();
            clTablaTitulo.BorderWidth = 0;
            clTablaTitulo.VerticalAlignment = Element.ALIGN_MIDDLE;
            clTablaTitulo.AddElement(tblTitulo);
            //===========

            PdfPTable tblEncabezado = new PdfPTable(3);
            tblEncabezado.WidthPercentage = 100;
            float[] widths = new float[] { 20f, 60f, 20f };
            tblEncabezado.SetWidths(widths);

            tblEncabezado.AddCell(clLogoSupIzq);
            tblEncabezado.AddCell(clTablaTitulo);
            tblEncabezado.AddCell(clLogoSupDer);
            //===========

            base.OnOpenDocument(writer, document);
            PdfPTable tabFot = new PdfPTable(new float[] { 1F });
            tabFot.SpacingAfter = 5F;
            PdfPCell cell;
            tabFot.TotalWidth = 560f;
            cell = new PdfPCell(tblEncabezado);
            cell.Border = Rectangle.NO_BORDER;
            tabFot.AddCell(cell);
            tabFot.WriteSelectedRows(0, -1, 20, document.Top + tabFot.TotalHeight + 10, writer.DirectContent);
            tabFot.SpacingAfter = 30f;

            var fontFooter = FontFactory.GetFont("Verdana", 8, Font.NORMAL, BaseColor.LIGHT_GRAY);
            //float cellHeight = 10f;
            PdfPTable footer = new PdfPTable(1);
            //footer.TotalWidth = 316f;
            //float[] cfWidths = new float[] { 2f, 1f };
            //footer.SetWidths(cfWidths);
            footer.TotalWidth = page.Width - 70;

            PdfPCell cf1 = new PdfPCell(new Phrase("1a Avenida Sur Oriente No. 290, Col.Centro C.P. 29000; Tuxtla Gutiérrez, Chiapas.", fontFooter));
            cf1.HorizontalAlignment = Element.ALIGN_LEFT;
            //cf1.FixedHeight = cellHeight;
            cf1.Border = PdfPCell.NO_BORDER;
            cf1.BorderWidthTop = 0.75f;
            cf1.BorderColor = BaseColor.LIGHT_GRAY;
            footer.AddCell(cf1);
            PdfPCell cf2 = new PdfPCell(new Phrase("Conmutador: 961 618 93 00 ext. red. 64009, 64115, 64138", fontFooter));
            cf2.HorizontalAlignment = Element.ALIGN_LEFT;
            cf2.Border = PdfPCell.NO_BORDER;
            footer.AddCell(cf2);
            PdfPCell cf3 = new PdfPCell(new Phrase("https://www.controldeconfianza.chiapas.gob.mx/", fontFooter));
            cf3.HorizontalAlignment = Element.ALIGN_LEFT;
            cf3.Border = PdfPCell.NO_BORDER;
            footer.AddCell(cf3);
            footer.WriteSelectedRows(0, -1, 20, 50, writer.DirectContent);
        }

    }

    public class HeaderReporteResguardo : PdfPageEventHelper
    {
        private string _Phrases;
        private string _Testigo;
        private string _Autorizo;

        public string phrases
        {
            get { return _Phrases; }
            set { _Phrases = value; }
        }

        public string testigo
        {
            get { return _Testigo; }
            set { _Testigo = value; }
        }

        public string autorizo
        {
            get { return _Autorizo; }
            set { _Autorizo = value; }
        }


        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            base.OnOpenDocument(writer, document);
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            //Rectangle page = document.PageSize.Rotate();
            Rectangle page = document.PageSize;

            //===========
            string imageizq = @"C:/inetpub/wwwroot/fotoUser/gobedohor.png";
            iTextSharp.text.Image jpgSupIzq = iTextSharp.text.Image.GetInstance(imageizq);
            jpgSupIzq.ScaleToFit(80f, 80f);

            PdfPCell clLogoSupIzq = new PdfPCell();
            clLogoSupIzq.BorderWidth = 0;
            clLogoSupIzq.VerticalAlignment = Element.ALIGN_BOTTOM;
            clLogoSupIzq.AddElement(jpgSupIzq);
            //==========

            //
            string imageder = @"C:/inetpub/wwwroot/fotoUser/nuevoCeccc.png";
            iTextSharp.text.Image jpgSupDer = iTextSharp.text.Image.GetInstance(imageder);
            jpgSupDer.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
            jpgSupDer.ScaleToFit(100f, 100f);

            PdfPCell clLogoSupDer = new PdfPCell();
            clLogoSupDer.BorderWidth = 0;
            clLogoSupDer.VerticalAlignment = Element.ALIGN_BOTTOM;
            clLogoSupDer.AddElement(jpgSupDer);
            //==========

            Chunk chkTit = new Chunk("Unidad de Informática", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
            Paragraph paragraph = new Paragraph();
            paragraph.Alignment = Element.ALIGN_CENTER;
            //paragraph.SetLeading(10, 1);
            paragraph.Add(chkTit);

            Chunk chkSub = new Chunk("SERVICIO SOPORTE TECNICO", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
            Paragraph paragraph1 = new Paragraph();
            paragraph1.Alignment = Element.ALIGN_CENTER;
            //paragraph.SetLeading(10, 1);
            paragraph1.Add(chkSub);

            PdfPCell clTitulo = new PdfPCell();
            clTitulo.BorderWidth = 0;
            clTitulo.AddElement(paragraph);

            PdfPCell clSubTit = new PdfPCell();
            clSubTit.BorderWidth = 0;
            clSubTit.AddElement(paragraph1);

            PdfPTable tblTitulo = new PdfPTable(1);
            tblTitulo.WidthPercentage = 100;
            tblTitulo.AddCell(clTitulo);
            tblTitulo.AddCell(clSubTit);

            PdfPCell clTablaTitulo = new PdfPCell();
            clTablaTitulo.BorderWidth = 0;
            clTablaTitulo.VerticalAlignment = Element.ALIGN_MIDDLE;
            clTablaTitulo.AddElement(tblTitulo);
            //===========

            PdfPTable tblEncabezado = new PdfPTable(3);
            tblEncabezado.WidthPercentage = 100;
            float[] widths = new float[] { 20f, 20f, 20f };
            tblEncabezado.SetWidths(widths);

            tblEncabezado.AddCell(clLogoSupIzq);
            tblEncabezado.AddCell(clTablaTitulo);
            tblEncabezado.AddCell(clLogoSupDer);
            //===========

            base.OnOpenDocument(writer, document);
            PdfPTable tabFot = new PdfPTable(new float[] { 1F });
            tabFot.SpacingAfter = 5F;
            PdfPCell cell;
            //ancho de la tabla
            tabFot.TotalWidth = 560;
            cell = new PdfPCell(tblEncabezado);
            cell.Border = Rectangle.NO_BORDER;
            tabFot.AddCell(cell);
            tabFot.WriteSelectedRows(0, -1, 20, document.Top + tabFot.TotalHeight + 10, writer.DirectContent);
            tabFot.SpacingAfter = 30f;

            var fontFooter = FontFactory.GetFont("Verdana", 8, Font.NORMAL, BaseColor.BLACK);
            PdfPTable footer = new PdfPTable(3);
            footer.TotalWidth = page.Width - 40;

            PdfPCell cf1 = new PdfPCell(new Phrase("Resguardante", fontFooter));
            cf1.HorizontalAlignment = Element.ALIGN_CENTER;
            cf1.Border = PdfPCell.NO_BORDER;
            cf1.BorderWidthTop = 0.75f;
            footer.AddCell(cf1);

            PdfPCell cf2 = new PdfPCell(new Phrase("Realizo ST", fontFooter));
            cf2.HorizontalAlignment = Element.ALIGN_CENTER;
            cf2.Border = PdfPCell.NO_BORDER;
            cf2.BorderWidthTop = 0.75f;
            footer.AddCell(cf2);

            PdfPCell cf3 = new PdfPCell(new Phrase("Vo. Bo.", fontFooter));
            cf3.HorizontalAlignment = Element.ALIGN_CENTER;
            cf3.Border = PdfPCell.NO_BORDER;
            cf3.BorderWidthTop = 0.75f;
            footer.AddCell(cf3);

            PdfPCell cf1b = new PdfPCell(new Phrase(_Phrases, fontFooter));
            cf1b.HorizontalAlignment = Element.ALIGN_CENTER;
            cf1b.Border = PdfPCell.NO_BORDER;
            footer.AddCell(cf1b);

            PdfPCell cf2b = new PdfPCell(new Phrase(_Testigo, fontFooter));
            cf2b.HorizontalAlignment = Element.ALIGN_CENTER;
            cf2b.Border = PdfPCell.NO_BORDER;
            footer.AddCell(cf2b);

            PdfPCell cf3b = new PdfPCell(new Phrase(_Autorizo, fontFooter));
            cf3b.HorizontalAlignment = Element.ALIGN_CENTER;
            cf3b.Border = PdfPCell.NO_BORDER;
            footer.AddCell(cf3b);

            footer.WriteSelectedRows(0, -1, 20, 50, writer.DirectContent);

            iTextSharp.text.Rectangle rect = writer.GetBoxSize("footer");
        }

        public static HeaderReporteResguardo getMultilineFooter(string Phrases, string realizo, string jefe)
        {
            HeaderReporteResguardo result = new HeaderReporteResguardo();

            result.phrases = Phrases;
            result.testigo = realizo;
            result.autorizo = jefe;

            return result;
        }
    }


}
