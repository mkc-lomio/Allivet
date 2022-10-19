using MediatR;
using System;
using System.Runtime.Serialization;


namespace Allivet.WebAPI.Application.VeterinaryLocationManagement.Commands
{
    [DataContract]
    public class CreateVeterinaryLocationCommand : IRequest<bool>
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string ContactNumber { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public DateTime DateCreated { get; set; }
        [DataMember]
        public Guid CreatedBy { get; set; }
        [DataMember]
        public DateTime DateModified { get; set; }
        [DataMember]
        public Guid ModifiedBy { get; set; }
    }
}
