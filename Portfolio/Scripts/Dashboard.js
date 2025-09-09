// Global variables for ASP.NET control client IDs
var hfActiveTabId, hfSkillIdId, hfProjectIdId;
var txtSkillNameId, ddlProficiencyId, txtSkillDescriptionId;
var txtProjectTitleId, txtTechnologiesId, txtProjectDescriptionId;
var txtImagePathId, txtWebsiteUrlId, txtGithubUrlId;

// Initialize control IDs when page loads
function initializeControlIds() {
    // Find control IDs (these will be set by server-side code)
    var controls = document.querySelectorAll('[id*="hfActiveTab"]');
    if (controls.length > 0) hfActiveTabId = controls[0].id;

    controls = document.querySelectorAll('[id*="hfSkillId"]');
    if (controls.length > 0) hfSkillIdId = controls[0].id;

    controls = document.querySelectorAll('[id*="hfProjectId"]');
    if (controls.length > 0) hfProjectIdId = controls[0].id;

    controls = document.querySelectorAll('[id*="txtSkillName"]');
    if (controls.length > 0) txtSkillNameId = controls[0].id;

    controls = document.querySelectorAll('[id*="ddlProficiency"]');
    if (controls.length > 0) ddlProficiencyId = controls[0].id;

    controls = document.querySelectorAll('[id*="txtSkillDescription"]');
    if (controls.length > 0) txtSkillDescriptionId = controls[0].id;

    controls = document.querySelectorAll('[id*="txtProjectTitle"]');
    if (controls.length > 0) txtProjectTitleId = controls[0].id;

    controls = document.querySelectorAll('[id*="txtTechnologies"]');
    if (controls.length > 0) txtTechnologiesId = controls[0].id;

    controls = document.querySelectorAll('[id*="txtProjectDescription"]');
    if (controls.length > 0) txtProjectDescriptionId = controls[0].id;

    controls = document.querySelectorAll('[id*="txtImagePath"]');
    if (controls.length > 0) txtImagePathId = controls[0].id;

    controls = document.querySelectorAll('[id*="txtWebsiteUrl"]');
    if (controls.length > 0) txtWebsiteUrlId = controls[0].id;

    controls = document.querySelectorAll('[id*="txtGithubUrl"]');
    if (controls.length > 0) txtGithubUrlId = controls[0].id;
}

// Tab Navigation
function showSection(sectionName, element) {
    // Hide all sections
    document.querySelectorAll('.content-section').forEach(section => {
        section.classList.remove('active');
    });

    // Remove active class from all tabs
    document.querySelectorAll('.nav-tab').forEach(tab => {
        tab.classList.remove('active');
    });

    // Show selected section
    document.getElementById(sectionName + '-section').classList.add('active');

    // Add active class to clicked tab
    if (element) {
        element.classList.add('active');
    }

    // Store active tab in hidden field
    if (hfActiveTabId) {
        document.getElementById(hfActiveTabId).value = sectionName;
    }
}

// Page load - restore active tab
window.onload = function () {
    initializeControlIds();

    if (hfActiveTabId) {
        var activeTab = document.getElementById(hfActiveTabId).value;
        if (activeTab) {
            showSectionOnLoad(activeTab);
        }
    }
}

function showSectionOnLoad(sectionName) {
    document.querySelectorAll('.content-section').forEach(section => {
        section.classList.remove('active');
    });
    document.querySelectorAll('.nav-tab').forEach(tab => {
        tab.classList.remove('active');
    });

    document.getElementById(sectionName + '-section').classList.add('active');
    var tabs = document.querySelectorAll('.nav-tab');
    if (sectionName === 'skills') {
        tabs[0].classList.add('active');
    } else if (sectionName === 'projects') {
        tabs[1].classList.add('active');
    } else if (sectionName === 'messages') {
        tabs[2].classList.add('active');
    }
}

// Modal Functions
function showAddSkillModal() {
    document.getElementById('skillModalTitle').textContent = 'Add New Skill';
    if (hfSkillIdId) {
        document.getElementById(hfSkillIdId).value = '0';
    }
    clearSkillForm();
    document.getElementById('skillModal').style.display = 'block';
}

function showAddProjectModal() {
    document.getElementById('projectModalTitle').textContent = 'Add New Project';
    if (hfProjectIdId) {
        document.getElementById(hfProjectIdId).value = '0';
    }
    clearProjectForm();
    document.getElementById('projectModal').style.display = 'block';
}

function closeModal(modalId) {
    document.getElementById(modalId).style.display = 'none';
}

function clearSkillForm() {
    if (txtSkillNameId) document.getElementById(txtSkillNameId).value = '';
    if (ddlProficiencyId) document.getElementById(ddlProficiencyId).value = '';
    if (txtSkillDescriptionId) document.getElementById(txtSkillDescriptionId).value = '';
}

function clearProjectForm() {
    if (txtProjectTitleId) document.getElementById(txtProjectTitleId).value = '';
    if (txtTechnologiesId) document.getElementById(txtTechnologiesId).value = '';
    if (txtProjectDescriptionId) document.getElementById(txtProjectDescriptionId).value = '';
    if (txtImagePathId) document.getElementById(txtImagePathId).value = '';
    if (txtWebsiteUrlId) document.getElementById(txtWebsiteUrlId).value = '';
    if (txtGithubUrlId) document.getElementById(txtGithubUrlId).value = '';
}

// Skill Operations - Client Side
function viewSkill(button) {
    const actionContainer = button.closest('.action-container');

    // Get data from data attributes
    const skillName = actionContainer.getAttribute('data-name') || 'N/A';
    const proficiency = actionContainer.getAttribute('data-proficiency') || 'N/A';
    const description = actionContainer.getAttribute('data-description') || 'No description provided';

    // Populate view modal
    document.getElementById('viewSkillName').textContent = skillName;
    document.getElementById('viewProficiency').textContent = proficiency;
    //document.getElementById('viewSkillDescription').textContent = description;

    // Show view modal
    document.getElementById('viewSkillModal').style.display = 'block';
}

function editSkill(button) {
    const actionContainer = button.closest('.action-container');

    // Get data from data attributes
    const skillId = actionContainer.getAttribute('data-id');
    const skillName = actionContainer.getAttribute('data-name') || '';
    const proficiency = actionContainer.getAttribute('data-proficiency') || '';
    const description = actionContainer.getAttribute('data-description') || '';

    // Set the skill ID for edit
    if (hfSkillIdId) {
        document.getElementById(hfSkillIdId).value = skillId;
    }

    // Populate edit modal
    if (txtSkillNameId) document.getElementById(txtSkillNameId).value = skillName;
    if (ddlProficiencyId) document.getElementById(ddlProficiencyId).value = proficiency;
    if (txtSkillDescriptionId) document.getElementById(txtSkillDescriptionId).value = description;

    // Set modal title and show
    document.getElementById('skillModalTitle').textContent = 'Edit Skill';
    document.getElementById('skillModal').style.display = 'block';
}

// Project Operations - Client Side
function viewProject(button) {
    const actionContainer = button.closest('.action-container');

    // Get data from data attributes
    const title = actionContainer.getAttribute('data-title') || 'N/A';
    const technologies = actionContainer.getAttribute('data-technologies') || 'N/A';
    const description = actionContainer.getAttribute('data-description') || 'No description provided';
    const imagePath = actionContainer.getAttribute('data-imagepath') || 'Not specified';
    const websiteUrl = actionContainer.getAttribute('data-websiteurl') || 'Not specified';
    const githubUrl = actionContainer.getAttribute('data-githuburl') || 'Not specified';

    // Populate view modal
    document.getElementById('viewTitle').textContent = title;
    document.getElementById('viewTechnologies').textContent = technologies;
    document.getElementById('viewDescription').textContent = description;
    document.getElementById('viewImagePath').textContent = imagePath;
    document.getElementById('viewWebsiteUrl').textContent = websiteUrl;
    document.getElementById('viewGithubUrl').textContent = githubUrl;

    // Show view modal
    document.getElementById('viewProjectModal').style.display = 'block';
}

function editProject(button) {
    const actionContainer = button.closest('.action-container');

    // Get data from data attributes
    const projectId = actionContainer.getAttribute('data-id');
    const title = actionContainer.getAttribute('data-title') || '';
    const technologies = actionContainer.getAttribute('data-technologies') || '';
    const description = actionContainer.getAttribute('data-description') || '';
    const imagePath = actionContainer.getAttribute('data-imagepath') || '';
    const websiteUrl = actionContainer.getAttribute('data-websiteurl') || '';
    const githubUrl = actionContainer.getAttribute('data-githuburl') || '';

    // Set the project ID for edit
    if (hfProjectIdId) {
        document.getElementById(hfProjectIdId).value = projectId;
    }

    // Populate edit modal
    if (txtProjectTitleId) document.getElementById(txtProjectTitleId).value = title;
    if (txtTechnologiesId) document.getElementById(txtTechnologiesId).value = technologies;
    if (txtProjectDescriptionId) document.getElementById(txtProjectDescriptionId).value = description;
    if (txtImagePathId) document.getElementById(txtImagePathId).value = imagePath;
    if (txtWebsiteUrlId) document.getElementById(txtWebsiteUrlId).value = websiteUrl;
    if (txtGithubUrlId) document.getElementById(txtGithubUrlId).value = githubUrl;

    // Set modal title and show
    document.getElementById('projectModalTitle').textContent = 'Edit Project';
    document.getElementById('projectModal').style.display = 'block';
}

// Contact Message Operations - Client Side
function viewMessage(button) {
    const actionContainer = button.closest('.action-container');

    // Get data from data attributes
    const messageId = actionContainer.getAttribute('data-id');
    const name = actionContainer.getAttribute('data-name') || 'N/A';
    const email = actionContainer.getAttribute('data-email') || 'N/A';
    const subject = actionContainer.getAttribute('data-subject') || 'N/A';
    const message = actionContainer.getAttribute('data-message') || 'No message content';
    const date = actionContainer.getAttribute('data-date') || 'N/A';
    const isRead = actionContainer.getAttribute('data-isread') === 'True';

    // Populate view modal
    document.getElementById('viewMessageName').textContent = name;
    document.getElementById('viewMessageEmail').textContent = email;
    document.getElementById('viewMessageSubject').textContent = subject;
    document.getElementById('viewMessageDate').textContent = date;
    document.getElementById('viewMessageContent').textContent = message;

    // Set up reply email link
    const replyLink = document.getElementById('replyEmailLink');
    const replySubject = encodeURIComponent('Re: ' + subject);
    const replyBody = encodeURIComponent('\n\n--- Original Message ---\nFrom: ' + name + '\nDate: ' + date + '\nSubject: ' + subject + '\n\n' + message);
    replyLink.href = `mailto:${email}?subject=${replySubject}&body=${replyBody}`;

    // Mark message as read if it's unread
    if (!isRead) {
        markMessageAsRead(messageId);
    }

    // Show view modal
    document.getElementById('viewMessageModal').style.display = 'block';
}

// Mark message as read using AJAX call (no page refresh)
function markMessageAsRead(messageId) {
    fetch('Dashboard.aspx/MarkMessageAsRead', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ messageId: parseInt(messageId) })
    })
        .then(response => response.json())
        .then(data => {
            if (data.d) {
                // Update the UI to show message as read
                updateMessageStatusUI(messageId);
                // Update unread badges
                updateUnreadBadges();
            }
        })
        .catch(error => {
            console.log('Error marking message as read:', error);
            // Continue showing modal even if marking as read fails
        });
}

// Update UI to show message as read
function updateMessageStatusUI(messageId) {
    const actionContainer = document.querySelector(`[data-id="${messageId}"]`);
    if (actionContainer) {
        // Update the status span in the row
        const statusSpan = actionContainer.closest('tr').querySelector('.status-unread, .status-read');
        if (statusSpan && statusSpan.classList.contains('status-unread')) {
            statusSpan.className = 'status-read';
            statusSpan.textContent = 'Read';
        }
        // Update data attribute
        actionContainer.setAttribute('data-isread', 'True');
    }
}

// Update unread count badges
function updateUnreadBadges() {
    // Get current unread count from UI
    const statusElements = document.querySelectorAll('.status-unread');
    const unreadCount = statusElements.length - 1; // Subtract 1 because we just marked one as read

    const unreadBadge = document.getElementById('unreadBadge');
    const tabBadge = document.getElementById('tabBadge');
    const lblUnreadCount = document.querySelector('[id*="lblUnreadCount"]');
    const lblTabUnreadCount = document.querySelector('[id*="lblTabUnreadCount"]');

    if (unreadCount > 0) {
        if (unreadBadge) unreadBadge.style.display = 'inline';
        if (tabBadge) tabBadge.style.display = 'inline';
        if (lblUnreadCount) lblUnreadCount.textContent = unreadCount;
        if (lblTabUnreadCount) lblTabUnreadCount.textContent = unreadCount;
    } else {
        if (unreadBadge) unreadBadge.style.display = 'none';
        if (tabBadge) tabBadge.style.display = 'none';
    }
}

// Close modal when clicking outside
window.onclick = function (event) {
    if (event.target.classList.contains('modal')) {
        event.target.style.display = 'none';
    }
}

document.addEventListener('keydown', (event) => {
    if (event.altKey && event.key === 'a') {
        event.preventDefault();
        window.location.href = 'https://localhost:44362/Default';
    }
});