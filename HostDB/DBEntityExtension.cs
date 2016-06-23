using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using HostDB;

namespace HostDB
{
public partial class Customer
    {
        public override Entity Fetch()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<Customer>();
                var ent = mem.GetById(Id);
                if (ent == null)
                {
                    using (var dc = new HostDBDataContext())
                    {
                        var sqlEnt = dc.GetTable<Customer>().Where(e => e.Id == Id).First();
                        if (sqlEnt != null)
                        {
                            return mem.Store(sqlEnt);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return ent;
                }
            }
        }

        public override Entity Update()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<Customer>();
                return mem.Store(this);
            }
        }

        public override void Delete()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<Customer>();
                mem.Delete(this);
            }
        }

        public override Entity AddSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<Customer>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override Entity UpdateSql()
        {
            using (var dc = new HostDBDataContext())
            {
                var ent = dc.GetTable<Customer>().Where(e => e.Id == Id).First();
                dc.GetTable<Customer>().DeleteOnSubmit(ent);
                dc.SubmitChanges();
                dc.GetTable<Customer>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override void DeleteSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<Customer>().DeleteOnSubmit(this);
                dc.SubmitChanges();
            }
        }
    }public partial class Venue
    {
        public override Entity Fetch()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<Venue>();
                var ent = mem.GetById(Id);
                if (ent == null)
                {
                    using (var dc = new HostDBDataContext())
                    {
                        var sqlEnt = dc.GetTable<Venue>().Where(e => e.Id == Id).First();
                        if (sqlEnt != null)
                        {
                            return mem.Store(sqlEnt);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return ent;
                }
            }
        }

        public override Entity Update()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<Venue>();
                return mem.Store(this);
            }
        }

        public override void Delete()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<Venue>();
                mem.Delete(this);
            }
        }

        public override Entity AddSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<Venue>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override Entity UpdateSql()
        {
            using (var dc = new HostDBDataContext())
            {
                var ent = dc.GetTable<Venue>().Where(e => e.Id == Id).First();
                dc.GetTable<Venue>().DeleteOnSubmit(ent);
                dc.SubmitChanges();
                dc.GetTable<Venue>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override void DeleteSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<Venue>().DeleteOnSubmit(this);
                dc.SubmitChanges();
            }
        }
    }public partial class CustomerOrder
    {
        public override Entity Fetch()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<CustomerOrder>();
                var ent = mem.GetById(Id);
                if (ent == null)
                {
                    using (var dc = new HostDBDataContext())
                    {
                        var sqlEnt = dc.GetTable<CustomerOrder>().Where(e => e.Id == Id).First();
                        if (sqlEnt != null)
                        {
                            return mem.Store(sqlEnt);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return ent;
                }
            }
        }

        public override Entity Update()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<CustomerOrder>();
                return mem.Store(this);
            }
        }

        public override void Delete()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<CustomerOrder>();
                mem.Delete(this);
            }
        }

        public override Entity AddSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<CustomerOrder>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override Entity UpdateSql()
        {
            using (var dc = new HostDBDataContext())
            {
                var ent = dc.GetTable<CustomerOrder>().Where(e => e.Id == Id).First();
                dc.GetTable<CustomerOrder>().DeleteOnSubmit(ent);
                dc.SubmitChanges();
                dc.GetTable<CustomerOrder>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override void DeleteSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<CustomerOrder>().DeleteOnSubmit(this);
                dc.SubmitChanges();
            }
        }
    }public partial class Location
    {
        public override Entity Fetch()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<Location>();
                var ent = mem.GetById(Id);
                if (ent == null)
                {
                    using (var dc = new HostDBDataContext())
                    {
                        var sqlEnt = dc.GetTable<Location>().Where(e => e.Id == Id).First();
                        if (sqlEnt != null)
                        {
                            return mem.Store(sqlEnt);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return ent;
                }
            }
        }

        public override Entity Update()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<Location>();
                return mem.Store(this);
            }
        }

        public override void Delete()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<Location>();
                mem.Delete(this);
            }
        }

        public override Entity AddSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<Location>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override Entity UpdateSql()
        {
            using (var dc = new HostDBDataContext())
            {
                var ent = dc.GetTable<Location>().Where(e => e.Id == Id).First();
                dc.GetTable<Location>().DeleteOnSubmit(ent);
                dc.SubmitChanges();
                dc.GetTable<Location>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override void DeleteSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<Location>().DeleteOnSubmit(this);
                dc.SubmitChanges();
            }
        }
    }public partial class Offer
    {
        public override Entity Fetch()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<Offer>();
                var ent = mem.GetById(Id);
                if (ent == null)
                {
                    using (var dc = new HostDBDataContext())
                    {
                        var sqlEnt = dc.GetTable<Offer>().Where(e => e.Id == Id).First();
                        if (sqlEnt != null)
                        {
                            return mem.Store(sqlEnt);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return ent;
                }
            }
        }

        public override Entity Update()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<Offer>();
                return mem.Store(this);
            }
        }

        public override void Delete()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<Offer>();
                mem.Delete(this);
            }
        }

        public override Entity AddSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<Offer>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override Entity UpdateSql()
        {
            using (var dc = new HostDBDataContext())
            {
                var ent = dc.GetTable<Offer>().Where(e => e.Id == Id).First();
                dc.GetTable<Offer>().DeleteOnSubmit(ent);
                dc.SubmitChanges();
                dc.GetTable<Offer>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override void DeleteSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<Offer>().DeleteOnSubmit(this);
                dc.SubmitChanges();
            }
        }
    }public partial class OfferVenue
    {
        public override Entity Fetch()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<OfferVenue>();
                var ent = mem.GetById(Id);
                if (ent == null)
                {
                    using (var dc = new HostDBDataContext())
                    {
                        var sqlEnt = dc.GetTable<OfferVenue>().Where(e => e.Id == Id).First();
                        if (sqlEnt != null)
                        {
                            return mem.Store(sqlEnt);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return ent;
                }
            }
        }

        public override Entity Update()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<OfferVenue>();
                return mem.Store(this);
            }
        }

        public override void Delete()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<OfferVenue>();
                mem.Delete(this);
            }
        }

        public override Entity AddSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<OfferVenue>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override Entity UpdateSql()
        {
            using (var dc = new HostDBDataContext())
            {
                var ent = dc.GetTable<OfferVenue>().Where(e => e.Id == Id).First();
                dc.GetTable<OfferVenue>().DeleteOnSubmit(ent);
                dc.SubmitChanges();
                dc.GetTable<OfferVenue>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override void DeleteSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<OfferVenue>().DeleteOnSubmit(this);
                dc.SubmitChanges();
            }
        }
    }public partial class Photo
    {
        public override Entity Fetch()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<Photo>();
                var ent = mem.GetById(Id);
                if (ent == null)
                {
                    using (var dc = new HostDBDataContext())
                    {
                        var sqlEnt = dc.GetTable<Photo>().Where(e => e.Id == Id).First();
                        if (sqlEnt != null)
                        {
                            return mem.Store(sqlEnt);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return ent;
                }
            }
        }

        public override Entity Update()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<Photo>();
                return mem.Store(this);
            }
        }

        public override void Delete()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<Photo>();
                mem.Delete(this);
            }
        }

        public override Entity AddSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<Photo>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override Entity UpdateSql()
        {
            using (var dc = new HostDBDataContext())
            {
                var ent = dc.GetTable<Photo>().Where(e => e.Id == Id).First();
                dc.GetTable<Photo>().DeleteOnSubmit(ent);
                dc.SubmitChanges();
                dc.GetTable<Photo>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override void DeleteSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<Photo>().DeleteOnSubmit(this);
                dc.SubmitChanges();
            }
        }
    }public partial class PhotoCategory
    {
        public override Entity Fetch()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<PhotoCategory>();
                var ent = mem.GetById(Id);
                if (ent == null)
                {
                    using (var dc = new HostDBDataContext())
                    {
                        var sqlEnt = dc.GetTable<PhotoCategory>().Where(e => e.Id == Id).First();
                        if (sqlEnt != null)
                        {
                            return mem.Store(sqlEnt);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return ent;
                }
            }
        }

        public override Entity Update()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<PhotoCategory>();
                return mem.Store(this);
            }
        }

        public override void Delete()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<PhotoCategory>();
                mem.Delete(this);
            }
        }

        public override Entity AddSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<PhotoCategory>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override Entity UpdateSql()
        {
            using (var dc = new HostDBDataContext())
            {
                var ent = dc.GetTable<PhotoCategory>().Where(e => e.Id == Id).First();
                dc.GetTable<PhotoCategory>().DeleteOnSubmit(ent);
                dc.SubmitChanges();
                dc.GetTable<PhotoCategory>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override void DeleteSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<PhotoCategory>().DeleteOnSubmit(this);
                dc.SubmitChanges();
            }
        }
    }public partial class Photographer
    {
        public override Entity Fetch()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<Photographer>();
                var ent = mem.GetById(Id);
                if (ent == null)
                {
                    using (var dc = new HostDBDataContext())
                    {
                        var sqlEnt = dc.GetTable<Photographer>().Where(e => e.Id == Id).First();
                        if (sqlEnt != null)
                        {
                            return mem.Store(sqlEnt);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return ent;
                }
            }
        }

        public override Entity Update()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<Photographer>();
                return mem.Store(this);
            }
        }

        public override void Delete()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<Photographer>();
                mem.Delete(this);
            }
        }

        public override Entity AddSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<Photographer>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override Entity UpdateSql()
        {
            using (var dc = new HostDBDataContext())
            {
                var ent = dc.GetTable<Photographer>().Where(e => e.Id == Id).First();
                dc.GetTable<Photographer>().DeleteOnSubmit(ent);
                dc.SubmitChanges();
                dc.GetTable<Photographer>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override void DeleteSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<Photographer>().DeleteOnSubmit(this);
                dc.SubmitChanges();
            }
        }
    }public partial class PhotographerExpertise
    {
        public override Entity Fetch()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<PhotographerExpertise>();
                var ent = mem.GetById(Id);
                if (ent == null)
                {
                    using (var dc = new HostDBDataContext())
                    {
                        var sqlEnt = dc.GetTable<PhotographerExpertise>().Where(e => e.Id == Id).First();
                        if (sqlEnt != null)
                        {
                            return mem.Store(sqlEnt);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return ent;
                }
            }
        }

        public override Entity Update()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<PhotographerExpertise>();
                return mem.Store(this);
            }
        }

        public override void Delete()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<PhotographerExpertise>();
                mem.Delete(this);
            }
        }

        public override Entity AddSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<PhotographerExpertise>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override Entity UpdateSql()
        {
            using (var dc = new HostDBDataContext())
            {
                var ent = dc.GetTable<PhotographerExpertise>().Where(e => e.Id == Id).First();
                dc.GetTable<PhotographerExpertise>().DeleteOnSubmit(ent);
                dc.SubmitChanges();
                dc.GetTable<PhotographerExpertise>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override void DeleteSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<PhotographerExpertise>().DeleteOnSubmit(this);
                dc.SubmitChanges();
            }
        }
    }public partial class PhotographerOffer
    {
        public override Entity Fetch()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<PhotographerOffer>();
                var ent = mem.GetById(Id);
                if (ent == null)
                {
                    using (var dc = new HostDBDataContext())
                    {
                        var sqlEnt = dc.GetTable<PhotographerOffer>().Where(e => e.Id == Id).First();
                        if (sqlEnt != null)
                        {
                            return mem.Store(sqlEnt);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return ent;
                }
            }
        }

        public override Entity Update()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<PhotographerOffer>();
                return mem.Store(this);
            }
        }

        public override void Delete()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<PhotographerOffer>();
                mem.Delete(this);
            }
        }

        public override Entity AddSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<PhotographerOffer>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override Entity UpdateSql()
        {
            using (var dc = new HostDBDataContext())
            {
                var ent = dc.GetTable<PhotographerOffer>().Where(e => e.Id == Id).First();
                dc.GetTable<PhotographerOffer>().DeleteOnSubmit(ent);
                dc.SubmitChanges();
                dc.GetTable<PhotographerOffer>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override void DeleteSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<PhotographerOffer>().DeleteOnSubmit(this);
                dc.SubmitChanges();
            }
        }
    }public partial class PhotoStyle
    {
        public override Entity Fetch()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<PhotoStyle>();
                var ent = mem.GetById(Id);
                if (ent == null)
                {
                    using (var dc = new HostDBDataContext())
                    {
                        var sqlEnt = dc.GetTable<PhotoStyle>().Where(e => e.Id == Id).First();
                        if (sqlEnt != null)
                        {
                            return mem.Store(sqlEnt);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return ent;
                }
            }
        }

        public override Entity Update()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<PhotoStyle>();
                return mem.Store(this);
            }
        }

        public override void Delete()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<PhotoStyle>();
                mem.Delete(this);
            }
        }

        public override Entity AddSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<PhotoStyle>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override Entity UpdateSql()
        {
            using (var dc = new HostDBDataContext())
            {
                var ent = dc.GetTable<PhotoStyle>().Where(e => e.Id == Id).First();
                dc.GetTable<PhotoStyle>().DeleteOnSubmit(ent);
                dc.SubmitChanges();
                dc.GetTable<PhotoStyle>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override void DeleteSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<PhotoStyle>().DeleteOnSubmit(this);
                dc.SubmitChanges();
            }
        }
    }public partial class PhotoType
    {
        public override Entity Fetch()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<PhotoType>();
                var ent = mem.GetById(Id);
                if (ent == null)
                {
                    using (var dc = new HostDBDataContext())
                    {
                        var sqlEnt = dc.GetTable<PhotoType>().Where(e => e.Id == Id).First();
                        if (sqlEnt != null)
                        {
                            return mem.Store(sqlEnt);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return ent;
                }
            }
        }

        public override Entity Update()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<PhotoType>();
                return mem.Store(this);
            }
        }

        public override void Delete()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<PhotoType>();
                mem.Delete(this);
            }
        }

        public override Entity AddSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<PhotoType>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override Entity UpdateSql()
        {
            using (var dc = new HostDBDataContext())
            {
                var ent = dc.GetTable<PhotoType>().Where(e => e.Id == Id).First();
                dc.GetTable<PhotoType>().DeleteOnSubmit(ent);
                dc.SubmitChanges();
                dc.GetTable<PhotoType>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override void DeleteSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<PhotoType>().DeleteOnSubmit(this);
                dc.SubmitChanges();
            }
        }
    }public partial class Tran
    {
        public override Entity Fetch()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<Tran>();
                var ent = mem.GetById(Id);
                if (ent == null)
                {
                    using (var dc = new HostDBDataContext())
                    {
                        var sqlEnt = dc.GetTable<Tran>().Where(e => e.Id == Id).First();
                        if (sqlEnt != null)
                        {
                            return mem.Store(sqlEnt);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return ent;
                }
            }
        }

        public override Entity Update()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<Tran>();
                return mem.Store(this);
            }
        }

        public override void Delete()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<Tran>();
                mem.Delete(this);
            }
        }

        public override Entity AddSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<Tran>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override Entity UpdateSql()
        {
            using (var dc = new HostDBDataContext())
            {
                var ent = dc.GetTable<Tran>().Where(e => e.Id == Id).First();
                dc.GetTable<Tran>().DeleteOnSubmit(ent);
                dc.SubmitChanges();
                dc.GetTable<Tran>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override void DeleteSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<Tran>().DeleteOnSubmit(this);
                dc.SubmitChanges();
            }
        }
    }public partial class TxnRecord
    {
        public override Entity Fetch()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<TxnRecord>();
                var ent = mem.GetById(Id);
                if (ent == null)
                {
                    using (var dc = new HostDBDataContext())
                    {
                        var sqlEnt = dc.GetTable<TxnRecord>().Where(e => e.Id == Id).First();
                        if (sqlEnt != null)
                        {
                            return mem.Store(sqlEnt);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return ent;
                }
            }
        }

        public override Entity Update()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<TxnRecord>();
                return mem.Store(this);
            }
        }

        public override void Delete()
        {
            using (var client = Entity.RedisManager.GetClient())
            {
                var mem = client.As<TxnRecord>();
                mem.Delete(this);
            }
        }

        public override Entity AddSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<TxnRecord>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override Entity UpdateSql()
        {
            using (var dc = new HostDBDataContext())
            {
                var ent = dc.GetTable<TxnRecord>().Where(e => e.Id == Id).First();
                dc.GetTable<TxnRecord>().DeleteOnSubmit(ent);
                dc.SubmitChanges();
                dc.GetTable<TxnRecord>().InsertOnSubmit(this);
                dc.SubmitChanges();
            }

            return this;
        }

        public override void DeleteSql()
        {
            using (var dc = new HostDBDataContext())
            {
                dc.GetTable<TxnRecord>().DeleteOnSubmit(this);
                dc.SubmitChanges();
            }
        }
    }}

