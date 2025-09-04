using System;
using System.Data;
using System.Web.UI;
using Portfolio.Helpers;

namespace Portfolio
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPortfolioData();
            }
        }

        private void LoadPortfolioData()
        {
            try
            {
                // Load All Skills
                DataTable skillsData = DBHelper.GetAllSkills();

                // Filter skills into categories
                DataTable frameworkSkills = skillsData.Clone();
                DataTable languageSkills = skillsData.Clone();

                foreach (DataRow row in skillsData.Rows)
                {
                    string skillName = row["SkillName"].ToString().ToLower();

                    // Categorize skills based on name
                    if (IsFrameworkSkill(skillName))
                    {
                        frameworkSkills.ImportRow(row);
                    }
                    else
                    {
                        languageSkills.ImportRow(row);
                    }
                }

                // Bind to repeaters
                rptFrameworkSkills.DataSource = frameworkSkills;
                rptFrameworkSkills.DataBind();

                rptLanguageSkills.DataSource = languageSkills;
                rptLanguageSkills.DataBind();

                // Load Projects
                DataTable projectsData = DBHelper.GetAllProjects();
                rptProjects.DataSource = projectsData;
                rptProjects.DataBind();
            }
            catch (Exception ex)
            {
                ShowContactMessage("Error loading portfolio data: " + ex.Message, false);
            }
        }

        // Helper method to categorize skills
        private bool IsFrameworkSkill(string skillName)
        {
            string[] frameworks = {
                "asp.net", ".net", "react", "angular", "vue", "laravel",
                "django", "flask", "express", "node.js", "bootstrap",
                "tailwind", "jquery", "next.js", "nuxt.js"
            };

            foreach (string framework in frameworks)
            {
                if (skillName.Contains(framework))
                {
                    return true;
                }
            }
            return false;
        }

        // Helper method to convert proficiency level to percentage
        public int GetProficiencyPercentage(string proficiency)
        {
            switch (proficiency.ToLower())
            {
                case "beginner":
                    return 25;
                case "intermediate":
                    return 50;
                case "advanced":
                    return 75;
                case "expert":
                    return 90;
                default:
                    return 0;
            }
        }

        // Helper method to format technologies as badges
        public string FormatTechnologies(string technologies)
        {
            if (string.IsNullOrEmpty(technologies))
                return "";

            string[] techArray = technologies.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            string result = "";

            foreach (string tech in techArray)
            {
                string cleanTech = tech.Trim();
                if (!string.IsNullOrEmpty(cleanTech))
                {
                    result += $"<span class='tech-badge'>{cleanTech}</span>";
                }
            }

            return result;
        }

        // Contact form submission
        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtContactName.Text.Trim();
                string email = txtContactEmail.Text.Trim();
                string subject = txtContactSubject.Text.Trim();
                string message = txtContactMessage.Text.Trim();

                // Basic validation
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) ||
                    string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(message))
                {
                    ShowContactMessage("Please fill in all fields.", false);
                    return;
                }

                // Email validation
                if (!IsValidEmail(email))
                {
                    ShowContactMessage("Please enter a valid email address.", false);
                    return;
                }

                // TODO: Implement email sending logic here
                // For now, we'll just show a success message
                // In a real application, you would:
                // 1. Save to database
                // 2. Send email notification
                // 3. Use SMTP to send emails

                ShowContactMessage("Thank you for your message! I'll get back to you soon.", true);
                ClearContactForm();
            }
            catch (Exception ex)
            {
                ShowContactMessage("Error sending message: " + ex.Message, false);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void ShowContactMessage(string message, bool isSuccess)
        {
            pnlContactMessage.Visible = true;
            lblContactMessage.Text = message;

            if (isSuccess)
            {
                lblContactMessage.CssClass = "contact-message success";
            }
            else
            {
                lblContactMessage.CssClass = "contact-message error";
            }

            // Scroll to contact section to show message
            string script = @"
                setTimeout(function() {
                    document.querySelector('#contact').scrollIntoView({ 
                        behavior: 'smooth', 
                        block: 'start' 
                    });
                }, 100);
            ";
            ClientScript.RegisterStartupScript(this.GetType(), "ScrollToContact", script, true);
        }

        private void ClearContactForm()
        {
            txtContactName.Text = "";
            txtContactEmail.Text = "";
            txtContactSubject.Text = "";
            txtContactMessage.Text = "";
        }
    }
}