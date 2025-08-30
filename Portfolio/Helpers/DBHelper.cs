using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Portfolio.Helpers
{
    public class DBHelper
    {
        private static readonly string connStr = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;

        // Get connection
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connStr);
        }

        // Execute SELECT query and return DataTable
        public static DataTable ExecuteReader(string query, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // Execute INSERT/UPDATE/DELETE
        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Execute scalar query
        public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }

        // ==================== ADMIN USERS ====================
        public static bool ValidateAdmin(string username, string password)
        {
            string query = "SELECT COUNT(*) FROM admin_users WHERE username = @username AND password = @password";
            SqlParameter[] parameters = {
                new SqlParameter("@username", username),
                new SqlParameter("@password", password)
            };
            return Convert.ToInt32(ExecuteScalar(query, parameters)) > 0;
        }

        // ==================== SKILLS MANAGEMENT ====================

        // Get all skills
        public static DataTable GetAllSkills()
        {
            string query = "SELECT * FROM Skills ORDER BY id ASC";
            return ExecuteReader(query);
        }

        // Get skill by ID
        public static DataTable GetSkillById(int id)
        {
            string query = "SELECT * FROM Skills WHERE id = @id";
            SqlParameter[] parameters = { new SqlParameter("@id", id) };
            return ExecuteReader(query, parameters);
        }

        // Add new skill
        public static bool AddSkill(string skillName, string proficiency, string description)
        {
            string query = "INSERT INTO Skills (SkillName, Proficiency, Description) VALUES (@skillName, @proficiency, @description)";
            SqlParameter[] parameters = {
                new SqlParameter("@skillName", skillName ?? (object)DBNull.Value),
                new SqlParameter("@proficiency", proficiency ?? (object)DBNull.Value),
                new SqlParameter("@description", description ?? (object)DBNull.Value)
            };
            return ExecuteNonQuery(query, parameters) > 0;
        }

        // Update skill
        public static bool UpdateSkill(int id, string skillName, string proficiency, string description)
        {
            string query = "UPDATE Skills SET SkillName = @skillName, Proficiency = @proficiency, Description = @description WHERE id = @id";
            SqlParameter[] parameters = {
                new SqlParameter("@id", id),
                new SqlParameter("@skillName", skillName ?? (object)DBNull.Value),
                new SqlParameter("@proficiency", proficiency ?? (object)DBNull.Value),
                new SqlParameter("@description", description ?? (object)DBNull.Value)
            };
            return ExecuteNonQuery(query, parameters) > 0;
        }

        // Delete skill
        public static bool DeleteSkill(int id)
        {
            string query = "DELETE FROM Skills WHERE id = @id";
            SqlParameter[] parameters = { new SqlParameter("@id", id) };
            return ExecuteNonQuery(query, parameters) > 0;
        }

        // Get skills by proficiency level
        public static DataTable GetSkillsByProficiency(string proficiency)
        {
            string query = "SELECT * FROM Skills WHERE Proficiency = @proficiency ORDER BY SkillName ASC";
            SqlParameter[] parameters = { new SqlParameter("@proficiency", proficiency) };
            return ExecuteReader(query, parameters);
        }

        // ==================== PROJECTS MANAGEMENT ====================

        // Get all projects
        public static DataTable GetAllProjects()
        {
            string query = "SELECT * FROM projects ORDER BY id ASC";
            return ExecuteReader(query);
        }

        // Get project by ID
        public static DataTable GetProjectById(int id)
        {
            string query = "SELECT * FROM projects WHERE id = @id";
            SqlParameter[] parameters = { new SqlParameter("@id", id) };
            return ExecuteReader(query, parameters);
        }

        // Add new project
        public static bool AddProject(string title, string shortDescription, string technologies, string imagePath, string websiteUrl, string githubUrl)
        {
            string query = @"INSERT INTO projects (title, short_description, technologies, image_path, website_url, github_url) 
                            VALUES (@title, @shortDescription, @technologies, @imagePath, @websiteUrl, @githubUrl)";
            SqlParameter[] parameters = {
                new SqlParameter("@title", title ?? (object)DBNull.Value),
                new SqlParameter("@shortDescription", shortDescription ?? (object)DBNull.Value),
                new SqlParameter("@technologies", technologies ?? (object)DBNull.Value),
                new SqlParameter("@imagePath", imagePath ?? (object)DBNull.Value),
                new SqlParameter("@websiteUrl", websiteUrl ?? (object)DBNull.Value),
                new SqlParameter("@githubUrl", githubUrl ?? (object)DBNull.Value)
            };
            return ExecuteNonQuery(query, parameters) > 0;
        }

        // Update project
        public static bool UpdateProject(int id, string title, string shortDescription, string technologies, string imagePath, string websiteUrl, string githubUrl)
        {
            string query = @"UPDATE projects SET title = @title, short_description = @shortDescription, 
                            technologies = @technologies, image_path = @imagePath, 
                            website_url = @websiteUrl, github_url = @githubUrl WHERE id = @id";
            SqlParameter[] parameters = {
                new SqlParameter("@id", id),
                new SqlParameter("@title", title ?? (object)DBNull.Value),
                new SqlParameter("@shortDescription", shortDescription ?? (object)DBNull.Value),
                new SqlParameter("@technologies", technologies ?? (object)DBNull.Value),
                new SqlParameter("@imagePath", imagePath ?? (object)DBNull.Value),
                new SqlParameter("@websiteUrl", websiteUrl ?? (object)DBNull.Value),
                new SqlParameter("@githubUrl", githubUrl ?? (object)DBNull.Value)
            };
            return ExecuteNonQuery(query, parameters) > 0;
        }

        // Delete project
        public static bool DeleteProject(int id)
        {
            string query = "DELETE FROM projects WHERE id = @id";
            SqlParameter[] parameters = { new SqlParameter("@id", id) };
            return ExecuteNonQuery(query, parameters) > 0;
        }

        // Get projects by technology
        public static DataTable GetProjectsByTechnology(string technology)
        {
            string query = "SELECT * FROM projects WHERE technologies LIKE @technology ORDER BY title ASC";
            SqlParameter[] parameters = { new SqlParameter("@technology", "%" + technology + "%") };
            return ExecuteReader(query, parameters);
        }

        // Get recent projects (limit by count)
        public static DataTable GetRecentProjects(int count = 6)
        {
            string query = "SELECT TOP (@count) * FROM projects ORDER BY id DESC";
            SqlParameter[] parameters = { new SqlParameter("@count", count) };
            return ExecuteReader(query, parameters);
        }

        // ==================== UTILITY METHODS ====================

        // Get total count of skills
        public static int GetSkillsCount()
        {
            string query = "SELECT COUNT(*) FROM Skills";
            return Convert.ToInt32(ExecuteScalar(query));
        }

        // Get total count of projects
        public static int GetProjectsCount()
        {
            string query = "SELECT COUNT(*) FROM projects";
            return Convert.ToInt32(ExecuteScalar(query));
        }
    }
}