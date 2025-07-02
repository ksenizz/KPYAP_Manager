using System;
using System.Windows;
using System.Windows.Controls;
using UsersApp.Models;
using System.Data.Entity;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using System.Linq;

namespace UsersApp.UserControl.Project
{
    public partial class ProjectDetailPage : System.Windows.Controls.UserControl
    {
        private readonly DB _context;
        private readonly ContentControl _overlayContentControl;
        public ProjectDetailPage(DB context, UsersApp.Models.Project project, ContentControl overlayContentControl)
        {
            InitializeComponent();
            _overlayContentControl = overlayContentControl;
            _context = context;

            if (project.Employees == null)
            {
                _context.Entry(project).Reference(p => p.Employees).Load();
            }
            DataContext = project;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _overlayContentControl.Content = new ProjectPage();
        }

        private void CreateReport_Click(object sender, RoutedEventArgs e)
        {
            var project = DataContext as UsersApp.Models.Project;
            if (project != null)
            {
                string filePath = @"C:\Users\HONOR\Desktop\ProjectReport.docx";
                CreateWordDocument(filePath, project);
                MessageBox.Show("Отчет создан успешно!");
            }
        }

        private void CreateWordDocument(string filePath, UsersApp.Models.Project project)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = new Body();

                Paragraph title = new Paragraph(new Run(new Text("Детали проекта")));
                title.ParagraphProperties = new ParagraphProperties(new ParagraphStyleId() { Val = "Title" });
                body.AppendChild(title);

                body.AppendChild(new Paragraph(new Run(new Text($"Название проекта: {project.Name}"))));
                body.AppendChild(new Paragraph(new Run(new Text($"Описание проекта: {project.Description}"))));
                body.AppendChild(new Paragraph(new Run(new Text($"Исполнитель: {project.Employees.FIO}"))));
                body.AppendChild(new Paragraph(new Run(new Text($"Статус: {project.ProjectStatus.Name}"))));
                body.AppendChild(new Paragraph(new Run(new Text($"Дата начала: {project.CreatedDate.ToString("dd.MM.yyyy")}"))));
                body.AppendChild(new Paragraph(new Run(new Text($"Дата окончания: {project.UpdatedDate.ToString("dd.MM.yyyy")}"))));

                body.AppendChild(new Paragraph(new Run(new Text("Задачи проекта:"))));

                var tasks = _context.Tasks.Where(t => t.ProjectId == project.Id).ToList();

                foreach (var task in tasks)
                {
                    body.AppendChild(new Paragraph(new Run(new Text($"Название задачи: {task.Name}"))));
                    body.AppendChild(new Paragraph(new Run(new Text($"Описание задачи: {task.Description}"))));
                    body.AppendChild(new Paragraph(new Run(new Text($"Срок выполнения: {task.Deadline.ToString("dd.MM.yyyy")}"))));
                    body.AppendChild(new Paragraph(new Run(new Text($"Статус задачи: {task.Status.Name}"))));
                    body.AppendChild(new Paragraph(new Run(new Text($"Ответственный: {task.Employee.FIO}"))));
                }

                mainPart.Document.AppendChild(body);
                mainPart.Document.Save();
            }
        }
    }
}