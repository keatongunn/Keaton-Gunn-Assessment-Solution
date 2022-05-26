

//Rough Class, Interface needs - BugReport, ServiceRequest, ServiceLevelAgreement, Calculation Strategies.

//IMPORTANT, Adding context for Calculation
public abstract class Ticket
{
    // Needs Service level agreement - Response Deadline (when the ticket needs to be assigned) & Breach Deadline (When the ticket should be resolved by)
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    //[Display(Name = "Created Date")]
    public DateTime CreatedDate { get; set; }
    //[Display(Name = "Updated Date")]
    public DateTime UpdatedDate { get; set; }
    public TicketPriority Priority { get; set; }
    public TicketStatus Status { get; set; }
    public TicketType Type { get; set; }
    public int ProjectId { get; set; }
    //public Project Project { get; set; }
    public string? DeveloperId { get; set; }
    //public ApplicationUser? Developer { get; set; }
    public string SubmitterId { get; set; }
    //public ApplicationUser Submitter { get; set; }
    //public ICollection<TicketHistory> TicketHistories { get; set; }
    //public ICollection<TicketComment> TicketComments { get; set; }
    //public ICollection<TicketNotification> TicketNotifications { get; set; }
    //public ICollection<TicketAttachment> TicketAttachments { get; set; }

    public ServiceLevelAgreement ServiceLevelAgreement { get; set; } //NEW

    public Ticket()
    {
        //TicketHistories = new HashSet<TicketHistory>();
        //TicketComments = new HashSet<TicketComment>();
        //TicketNotifications = new HashSet<TicketNotification>();
        //TicketAttachments = new HashSet<TicketAttachment>();
    }
}

//STRATEGIES

public interface CalculationStrategy //NEW
{
    public void ResponseDeadline(TicketPriority ticketPriority, ServiceLevelAgreement sla);
    public void BreachDeadline(TicketPriority ticketPriority, ServiceLevelAgreement sla);

}

public class BugReportStrategy : CalculationStrategy
{
    public void ResponseDeadline(TicketPriority ticketPriority, ServiceLevelAgreement sla)
    {
        //Holds the parameters for each specific priority, for example if the priority is high, you could have a base multiplier of 5 for the priority.
        // Bug report base multiplier could be 2
        // sla.ResponseDeadline = 5 (Priority) * 2 (Bug ReportBase)
    }

    public void BreachDeadline(TicketPriority ticketPriority, ServiceLevelAgreement sla)
    {
        //Follows the same suit as the 
    }
}

public class ServiceRequestStrategy : CalculationStrategy
{
    public void ResponseDeadline(TicketPriority ticketPriority, ServiceLevelAgreement sla)
    {
        // Implement the same as BugReport with a different base multiplier
    }
    public void BreachDeadline(TicketPriority ticketPriority, ServiceLevelAgreement sla)
    {

    }
}

public class ServiceLevelAgreement
{
    // Recorded in hours stored as an int for this assessment 
    public int ResponseDeadline { get; set; } // When the ticket needs to be assigned
    public int BreachDeadline { get; set; } // When the ticket should be resolved by
}

public enum ServiceRequestType //NEW
{
    GeneralMaintenance, 
    ComputerFailure, 
    ComputerBurstIntoFlames
}

public class BugReport : Ticket //NEW
{
    public string ErrorCode { get; set; }
    public List<string> ErrorLogs { get; set; }
}

public class ServiceRequest : Ticket //NEW
{
    public ServiceRequestType TypeOfRequest { get; set; }

}

public abstract class ServiceDecorator : ServiceRequest
{
    public ServiceRequest ServiceRequest { get; set; }
} 

public class ComputerOnFireDecorator : ServiceDecorator
{
    public ComputerOnFireDecorator(ServiceRequest serviceRequest)
    {
        ServiceRequest = serviceRequest;
    }

    // Add an additional X amount of hours to extinguish the fire and return it back to its original state
}

public enum TicketPriority
{
    High,
    Medium,
    Low
};

public enum TicketType //Not sufficient
{
    CrashReport,
    AccountIssue,
    GeneralQuestion
};

public enum TicketStatus
{
    Unopened,
    Opened,
    OnHold,
    Completed
}

/* Notes to explain my thought process - 
 * Use the compression strategy for the main portion of this assessment - 
 * Make class BugReport and ServiceRequest that inherit from Ticket - 
 * Bug report holds Error logs and error codes, service request a new enum TypeOfRequest 
 * (GeneralMaintenance, 
    ComputerFailure, 
    ComputerBurstIntoFlames)
 * For the second part, Add ServiceLevelAgreement class that holds both int's ResponseDeadline and BreachDeadline
 * introduce the Interface CalculationStrategy that holds the ResponseDeadline method, and the BreachDeadline method which would take the Priority, and the ServiceLevelAgreement class as parameters
 * Now add BugReportStrategy and ServiceRequest strategy that inherit from CalculationStrategy
 * Each method in both bug report and service request holds the parameters for each specific priority, for example if the priority is high, you could have a base multiplier of 5 for the priority. (probably use an if statement eg. if(priority == high) {
        // Bug report base multiplier could be 2
        // sla.ResponseDeadline = 5 (Priority) * 2 (Bug ReportBase)
 * Add an abstract class calculation context so the calculation will be implemented on run-time, then context for both ServiceRequest and Bug report that inherit from Calculation context to implement.
 * Use the add on decorator design pattern to add another layer of modification to ServiceRequest. For example, add abstract class ServiceDecorator : ServiceRequest, that holds a service request object, then for each type of service request (the new enum property) make use of a decorator. Eg. ComputerOnFireDecorator : ServiceDecorator would add the hours needed to extinguish the flames to the breach deadline. 
 */ 

