using System;
using System.Collections.Generic;
using System.Text;

public class DoctorNote
{
    public int Id { get; set; }

    public string PatientId { get; set; }

    public string DoctorId { get; set; }

    public string Note { get; set; }

    public DateTime CreatedDate { get; set; }
}