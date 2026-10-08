# Database ER Diagram (Mermaid)

This file contains an Entity-Relationship diagram (Mermaid) for the application's database model as defined in CustomerSupportContext and the model classes.

To render the diagram you can paste the Mermaid block into any Mermaid renderer (VS Code with Mermaid Preview, mermaid.live, or GitHub Markdown preview for mermaid-enabled renderers).

```mermaid
erDiagram
	USERROLE {
		int RoleId PK
		string RoleName
	}

	USERPROFILE {
		int UserId PK
		string UserName
		string Email
		string Password
		int RoleId FK
		string RoleName
		datetime CreatedDate
		datetime UpdatedDate
	}

	PRIORITY {
		int PriorityId PK
		string PriorityType
	}

	STATUS {
		int StatusId PK
		string StatusType
	}

	REQUEST {
		int RequestId PK
		int StatusId FK
		string Subject
		int AssignedTo FK
		int CustomerId FK
		string Description
		datetime CreatedDate
		datetime UpdatedDate
		int PriorityId FK
	}

	REQUESTCOMMENT {
		int CommentId PK
		int RequestId FK
		int UserId FK
		string Comments
		datetime CreatedDate
	}

	%% Relationships
	USERROLE ||--o{ USERPROFILE : "has"
	USERPROFILE ||--o{ REQUEST : "assigned_to/created_requests"
	USERPROFILE ||--o{ REQUESTCOMMENT : "author_of"
	PRIORITY ||--o{ REQUEST : "priority_of"
	STATUS ||--o{ REQUEST : "status_of"
	REQUEST ||--o{ REQUESTCOMMENT : "has_comments"

``` 

Summary of relationships
- UserRole (1) — (N) UserProfile via RoleId
- UserProfile (1) — (N) Request (as Customer via CustomerId)
- UserProfile (1) — (N) Request (as AssignedTo via AssignedTo)
- Request (1) — (N) RequestComment via RequestId
- UserProfile (1) — (N) RequestComment via UserId
- Priority (1) — (N) Request via PriorityId
- Status (1) — (N) Request via StatusId

Notes
- The diagram reflects the EF Core model (CustomerSupportContext.OnModelCreating).
- Primary/foreign keys, nullable FKs and required relationships are indicated by FK labels; Status and Role relationships are configured as required in the model where noted.
- If you want a PNG/SVG export, open this file in a Mermaid-capable renderer and export the diagram.
