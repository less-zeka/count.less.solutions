using count.less.solutions.Models.Domain;
using NHibernate.Event;
using NHibernate.Persister.Entity;
using System;
using System.Security.Principal;

namespace count.less.solutions.Persistence
{
    public class AuditEventListener : IPreUpdateEventListener, IPreInsertEventListener
    {
        public bool OnPreUpdate(PreUpdateEvent theEvent)
        {
            var audit = theEvent.Entity as IHaveAuditInformation;
            if (audit == null)
                return false;

            var time = DateTime.Now;
            //var name = WindowsIdentity.GetCurrent().Name;

            Set(theEvent.Persister, theEvent.State, "UpdatedAt", time);
            //Set(theEvent.Persister, theEvent.State, "UpdatedBy", name);

            audit.UpdatedAt = time;
            //audit.UpdatedBy = name;

            return false;
        }

        public bool OnPreInsert(PreInsertEvent @event)
        {
            var audit = @event.Entity as IHaveAuditInformation;
            if (audit == null)
                return false;


            var time = DateTime.Now;
            //var name = WindowsIdentity.GetCurrent().Name;

            Set(@event.Persister, @event.State, "CreatedAt", time);
            Set(@event.Persister, @event.State, "UpdatedAt", time);
            //Set(@event.Persister, @event.State, "CreatedBy", name);
            //Set(@event.Persister, @event.State, "UpdatedBy", name);

            audit.CreatedAt = time;
            //audit.CreatedBy = name;
            audit.UpdatedAt = time;
            //audit.UpdatedBy = name;

            return false;
        }

        private void Set(IEntityPersister persister, object[] state, string propertyName, object value)
        {
            var index = Array.IndexOf(persister.PropertyNames, propertyName);
            if (index == -1)
                return;
            state[index] = value;
        }
    }
}