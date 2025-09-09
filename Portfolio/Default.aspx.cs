using Portfolio.Helpers;
using System;
using System.Data;
using System.Runtime.InteropServices.ComTypes;
using System.Web.UI;

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

        #region Portfolio Data Loading
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

                    if (IsFrameworkSkill(skillName))
                        frameworkSkills.ImportRow(row);
                    else
                        languageSkills.ImportRow(row);
                }

                // Bind language/framework skills to repeaters
                rptLanguageSkills.DataSource = languageSkills;
                rptLanguageSkills.DataBind();

                // Bind framework skills to repeater (this was missing!)
                rptFrameworkSkills.DataSource = frameworkSkills;
                rptFrameworkSkills.DataBind();

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
                    return true;
            }
            return false;
        }

        public int GetProficiencyPercentage(string proficiency)
        {
            return int.Parse(proficiency);
        }

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
        #endregion

        #region Contact Form
        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtContactName.Text.Trim();
                string email = txtContactEmail.Text.Trim();
                string subject = txtContactSubject.Text.Trim();
                string message = txtContactMessage.Text.Trim();

                // Validation
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) ||
                    string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(message))
                {
                    ShowContactMessage("Please fill in all fields.", false);
                    return;
                }

                if (!IsValidEmail(email))
                {
                    ShowContactMessage("Please enter a valid email address.", false);
                    return;
                }

                // Save message to DB
                bool success = DBHelper.AddContactMessage(name, email, subject, message);

                if (success)
                {
                    ShowContactMessage("Thank you for your message!", true);
                    ClearContactForm();
                }
                else
                {
                    ShowContactMessage("Sorry, there was an error sending your message. Please try again.", false);
                }
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
            lblContactMessage.CssClass = isSuccess
                ? "contact-message success"
                : "contact-message error";

            // Scroll to contact section and fade out success message after 3 seconds
            string script = @"
                setTimeout(function() {
                    document.querySelector('#contact').scrollIntoView({ 
                        behavior: 'smooth', 
                        block: 'start' 
                    });
                }, 100);
        
                // Fade out success message after 3 seconds
                " + (isSuccess ? @"
                setTimeout(function() {
                    var messagePanel = document.getElementById('" + pnlContactMessage.ClientID + @"');
                    if (messagePanel) {
                        messagePanel.style.transition = 'opacity 0.5s ease-out';
                        messagePanel.style.opacity = '0';
                
                        // Completely hide after fade completes
                        setTimeout(function() {
                            messagePanel.style.display = 'none';
                        }, 500);
                    }
                }, 3000);" : "") + @"
            ";

            ScriptManager.RegisterStartupScript(this, GetType(), "ScrollToContact", script, true);
        }

        private void ClearContactForm()
        {
            txtContactName.Text = "";
            txtContactEmail.Text = "";
            txtContactSubject.Text = "";
            txtContactMessage.Text = "";
        }
        #endregion
    }
}