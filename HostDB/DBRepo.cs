using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace HostDB
{
    public partial class PhotographerWorkPicture
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("PhotographerPictureId", (object)PhotographerPictureId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotographerWorkId", (object)PhotographerWorkId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PictureName", (object)PictureName??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Description", (object)Description??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Path", (object)Path??DBNull.Value);
            sqlComm.Parameters.AddWithValue("SortOrder", (object)SortOrder??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO PhotographerWorkPicture ( PhotographerPictureId,PhotographerWorkId,PictureName,Description,Path,SortOrder ) values (@PhotographerPictureId,@PhotographerWorkId,@PictureName,@Description,@Path,@SortOrder)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("PhotographerPictureId", (object)PhotographerPictureId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotographerWorkId", (object)PhotographerWorkId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PictureName", (object)PictureName??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Description", (object)Description??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Path", (object)Path??DBNull.Value);
            sqlComm.Parameters.AddWithValue("SortOrder", (object)SortOrder??DBNull.Value);

            sqlComm.CommandText ="UPDATE PhotographerWorkPicture SET PhotographerPictureId = @PhotographerPictureId, PhotographerWorkId = @PhotographerWorkId, PictureName = @PictureName, Description = @Description, Path = @Path, SortOrder = @SortOrder where PhotographerPictureId = @PhotographerPictureId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM PhotographerWorkPicture  where PhotographerPictureId = @PhotographerPictureId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<PhotographerWorkPicture>().Where(e =>  e.PhotographerPictureId == PhotographerPictureId ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            PhotographerWorkPicture curEntity = currentEntity as PhotographerWorkPicture;

            if( curEntity.PhotographerPictureId == PhotographerPictureId )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class OfferPicture
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("OfferPictureId", (object)OfferPictureId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("OfferId", (object)OfferId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PictureName", (object)PictureName??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Description", (object)Description??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Path", (object)Path??DBNull.Value);
            sqlComm.Parameters.AddWithValue("SortOrder", (object)SortOrder??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO OfferPicture ( OfferPictureId,OfferId,PictureName,Description,Path,SortOrder ) values (@OfferPictureId,@OfferId,@PictureName,@Description,@Path,@SortOrder)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("OfferPictureId", (object)OfferPictureId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("OfferId", (object)OfferId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PictureName", (object)PictureName??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Description", (object)Description??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Path", (object)Path??DBNull.Value);
            sqlComm.Parameters.AddWithValue("SortOrder", (object)SortOrder??DBNull.Value);

            sqlComm.CommandText ="UPDATE OfferPicture SET OfferPictureId = @OfferPictureId, OfferId = @OfferId, PictureName = @PictureName, Description = @Description, Path = @Path, SortOrder = @SortOrder where OfferPictureId = @OfferPictureId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM OfferPicture  where OfferPictureId = @OfferPictureId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<OfferPicture>().Where(e =>  e.OfferPictureId == OfferPictureId ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            OfferPicture curEntity = currentEntity as OfferPicture;

            if( curEntity.OfferPictureId == OfferPictureId )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class CustomerSession
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("SessionId", (object)SessionId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("SessionKey", (object)SessionKey??DBNull.Value);
            sqlComm.Parameters.AddWithValue("CustomerId", (object)CustomerId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Status", (object)Status??DBNull.Value);
            sqlComm.Parameters.AddWithValue("LastUseTime", (object)LastUseTime??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO CustomerSession ( SessionId,SessionKey,CustomerId,Status,LastUseTime ) values (@SessionId,@SessionKey,@CustomerId,@Status,@LastUseTime)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("SessionId", (object)SessionId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("SessionKey", (object)SessionKey??DBNull.Value);
            sqlComm.Parameters.AddWithValue("CustomerId", (object)CustomerId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Status", (object)Status??DBNull.Value);
            sqlComm.Parameters.AddWithValue("LastUseTime", (object)LastUseTime??DBNull.Value);

            sqlComm.CommandText ="UPDATE CustomerSession SET SessionId = @SessionId, SessionKey = @SessionKey, CustomerId = @CustomerId, Status = @Status, LastUseTime = @LastUseTime where SessionId = @SessionId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM CustomerSession  where SessionId = @SessionId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<CustomerSession>().Where(e =>  e.SessionId == SessionId ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            CustomerSession curEntity = currentEntity as CustomerSession;

            if( curEntity.SessionId == SessionId )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class City
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("CityId", (object)CityId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("CityName", (object)CityName??DBNull.Value);
            sqlComm.Parameters.AddWithValue("SortOrder", (object)SortOrder??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO City ( CityId,CityName,SortOrder ) values (@CityId,@CityName,@SortOrder)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("CityId", (object)CityId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("CityName", (object)CityName??DBNull.Value);
            sqlComm.Parameters.AddWithValue("SortOrder", (object)SortOrder??DBNull.Value);

            sqlComm.CommandText ="UPDATE City SET CityId = @CityId, CityName = @CityName, SortOrder = @SortOrder where CityId = @CityId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM City  where CityId = @CityId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<City>().Where(e =>  e.CityId == CityId ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            City curEntity = currentEntity as City;

            if( curEntity.CityId == CityId )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class PhotographerSession
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("SessionId", (object)SessionId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("SessionKey", (object)SessionKey??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotographerId", (object)PhotographerId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Status", (object)Status??DBNull.Value);
            sqlComm.Parameters.AddWithValue("LastUseTime", (object)LastUseTime??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO PhotographerSession ( SessionId,SessionKey,PhotographerId,Status,LastUseTime ) values (@SessionId,@SessionKey,@PhotographerId,@Status,@LastUseTime)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("SessionId", (object)SessionId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("SessionKey", (object)SessionKey??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotographerId", (object)PhotographerId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Status", (object)Status??DBNull.Value);
            sqlComm.Parameters.AddWithValue("LastUseTime", (object)LastUseTime??DBNull.Value);

            sqlComm.CommandText ="UPDATE PhotographerSession SET SessionId = @SessionId, SessionKey = @SessionKey, PhotographerId = @PhotographerId, Status = @Status, LastUseTime = @LastUseTime where SessionId = @SessionId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM PhotographerSession  where SessionId = @SessionId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<PhotographerSession>().Where(e =>  e.SessionId == SessionId ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            PhotographerSession curEntity = currentEntity as PhotographerSession;

            if( curEntity.SessionId == SessionId )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class Remark
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("RemarkId", (object)RemarkId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Details", (object)Details??DBNull.Value);
            sqlComm.Parameters.AddWithValue("CustomerId", (object)CustomerId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Level", (object)Level??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO Remark ( RemarkId,Details,CustomerId,Level ) values (@RemarkId,@Details,@CustomerId,@Level)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("RemarkId", (object)RemarkId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Details", (object)Details??DBNull.Value);
            sqlComm.Parameters.AddWithValue("CustomerId", (object)CustomerId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Level", (object)Level??DBNull.Value);

            sqlComm.CommandText ="UPDATE Remark SET RemarkId = @RemarkId, Details = @Details, CustomerId = @CustomerId, Level = @Level where RemarkId = @RemarkId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM Remark  where RemarkId = @RemarkId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<Remark>().Where(e =>  e.RemarkId == RemarkId ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            Remark curEntity = currentEntity as Remark;

            if( curEntity.RemarkId == RemarkId )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class Venue
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("VenueId", (object)VenueId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("VenueName", (object)VenueName??DBNull.Value);
            sqlComm.Parameters.AddWithValue("LocationId", (object)LocationId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("SortOrder", (object)SortOrder??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO Venue ( VenueId,VenueName,LocationId,SortOrder ) values (@VenueId,@VenueName,@LocationId,@SortOrder)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("VenueId", (object)VenueId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("VenueName", (object)VenueName??DBNull.Value);
            sqlComm.Parameters.AddWithValue("LocationId", (object)LocationId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("SortOrder", (object)SortOrder??DBNull.Value);

            sqlComm.CommandText ="UPDATE Venue SET VenueId = @VenueId, VenueName = @VenueName, LocationId = @LocationId, SortOrder = @SortOrder where VenueId = @VenueId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM Venue  where VenueId = @VenueId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<Venue>().Where(e =>  e.VenueId == VenueId ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            Venue curEntity = currentEntity as Venue;

            if( curEntity.VenueId == VenueId )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class OfferVenue
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("OfferId", (object)OfferId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("VenueId", (object)VenueId??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO OfferVenue ( OfferId,VenueId ) values (@OfferId,@VenueId)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("OfferId", (object)OfferId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("VenueId", (object)VenueId??DBNull.Value);

            sqlComm.CommandText ="UPDATE OfferVenue SET OfferId = @OfferId, VenueId = @VenueId where OfferId = @OfferId and VenueId = @VenueId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM OfferVenue  where OfferId = @OfferId and VenueId = @VenueId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<OfferVenue>().Where(e =>  e.OfferId == OfferId && e.VenueId == VenueId ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            OfferVenue curEntity = currentEntity as OfferVenue;

            if( curEntity.OfferId == OfferId && curEntity.VenueId == VenueId )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class GlobalValue
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("GlbName", (object)GlbName??DBNull.Value);
            sqlComm.Parameters.AddWithValue("GlbValue", (object)GlbValue??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO GlobalValue ( GlbName,GlbValue ) values (@GlbName,@GlbValue)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("GlbName", (object)GlbName??DBNull.Value);
            sqlComm.Parameters.AddWithValue("GlbValue", (object)GlbValue??DBNull.Value);

            sqlComm.CommandText ="UPDATE GlobalValue SET GlbName = @GlbName, GlbValue = @GlbValue where GlbName = @GlbName ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM GlobalValue  where GlbName = @GlbName ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<GlobalValue>().Where(e =>  e.GlbName == GlbName ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            GlobalValue curEntity = currentEntity as GlobalValue;

            if( curEntity.GlbName == GlbName )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class Photo
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("PhotoId", (object)PhotoId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("CustomerOrderId", (object)CustomerOrderId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotoName", (object)PhotoName??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Description", (object)Description??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Path", (object)Path??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Comment", (object)Comment??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Selected", (object)Selected??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Retouched", (object)Retouched??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Confirmed", (object)Confirmed??DBNull.Value);
            sqlComm.Parameters.AddWithValue("SortOrder", (object)SortOrder??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO Photo ( PhotoId,CustomerOrderId,PhotoName,Description,Path,Comment,Selected,Retouched,Confirmed,SortOrder ) values (@PhotoId,@CustomerOrderId,@PhotoName,@Description,@Path,@Comment,@Selected,@Retouched,@Confirmed,@SortOrder)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("PhotoId", (object)PhotoId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("CustomerOrderId", (object)CustomerOrderId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotoName", (object)PhotoName??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Description", (object)Description??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Path", (object)Path??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Comment", (object)Comment??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Selected", (object)Selected??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Retouched", (object)Retouched??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Confirmed", (object)Confirmed??DBNull.Value);
            sqlComm.Parameters.AddWithValue("SortOrder", (object)SortOrder??DBNull.Value);

            sqlComm.CommandText ="UPDATE Photo SET PhotoId = @PhotoId, CustomerOrderId = @CustomerOrderId, PhotoName = @PhotoName, Description = @Description, Path = @Path, Comment = @Comment, Selected = @Selected, Retouched = @Retouched, Confirmed = @Confirmed, SortOrder = @SortOrder where PhotoId = @PhotoId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM Photo  where PhotoId = @PhotoId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<Photo>().Where(e =>  e.PhotoId == PhotoId ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            Photo curEntity = currentEntity as Photo;

            if( curEntity.PhotoId == PhotoId )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class NewsArticle
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("NewsId", (object)NewsId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Title", (object)Title??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Content", (object)Content??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Time", (object)Time??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Photo", (object)Photo??DBNull.Value);
            sqlComm.Parameters.AddWithValue("SortOrder", (object)SortOrder??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO NewsArticle ( NewsId,Title,Content,Time,Photo,SortOrder ) values (@NewsId,@Title,@Content,@Time,@Photo,@SortOrder)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("NewsId", (object)NewsId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Title", (object)Title??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Content", (object)Content??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Time", (object)Time??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Photo", (object)Photo??DBNull.Value);
            sqlComm.Parameters.AddWithValue("SortOrder", (object)SortOrder??DBNull.Value);

            sqlComm.CommandText ="UPDATE NewsArticle SET NewsId = @NewsId, Title = @Title, Content = @Content, Time = @Time, Photo = @Photo, SortOrder = @SortOrder where NewsId = @NewsId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM NewsArticle  where NewsId = @NewsId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<NewsArticle>().Where(e =>  e.NewsId == NewsId ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            NewsArticle curEntity = currentEntity as NewsArticle;

            if( curEntity.NewsId == NewsId )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class CustomerOrder
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("SerialNo", (object)SerialNo??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotographerId", (object)PhotographerId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("CustomerId", (object)CustomerId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("OfferId", (object)OfferId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("AppointmentTime", (object)AppointmentTime??DBNull.Value);
            sqlComm.Parameters.AddWithValue("OrderTime", (object)OrderTime??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Amount", (object)Amount??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotographerPay", (object)PhotographerPay??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Status", (object)Status??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Paid", (object)Paid??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO CustomerOrder ( SerialNo,PhotographerId,CustomerId,OfferId,AppointmentTime,OrderTime,Amount,PhotographerPay,Status,Paid ) values (@SerialNo,@PhotographerId,@CustomerId,@OfferId,@AppointmentTime,@OrderTime,@Amount,@PhotographerPay,@Status,@Paid)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("SerialNo", (object)SerialNo??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotographerId", (object)PhotographerId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("CustomerId", (object)CustomerId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("OfferId", (object)OfferId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("AppointmentTime", (object)AppointmentTime??DBNull.Value);
            sqlComm.Parameters.AddWithValue("OrderTime", (object)OrderTime??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Amount", (object)Amount??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotographerPay", (object)PhotographerPay??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Status", (object)Status??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Paid", (object)Paid??DBNull.Value);

            sqlComm.CommandText ="UPDATE CustomerOrder SET SerialNo = @SerialNo, PhotographerId = @PhotographerId, CustomerId = @CustomerId, OfferId = @OfferId, AppointmentTime = @AppointmentTime, OrderTime = @OrderTime, Amount = @Amount, PhotographerPay = @PhotographerPay, Status = @Status, Paid = @Paid where SerialNo = @SerialNo ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM CustomerOrder  where SerialNo = @SerialNo ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<CustomerOrder>().Where(e =>  e.SerialNo == SerialNo ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            CustomerOrder curEntity = currentEntity as CustomerOrder;

            if( curEntity.SerialNo == SerialNo )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class Photographer
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("PhotographerId", (object)PhotographerId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Email", (object)Email??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotographerName", (object)PhotographerName??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Password", (object)Password??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PayRate", (object)PayRate??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Gender", (object)Gender??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Age", (object)Age??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Phone", (object)Phone??DBNull.Value);
            sqlComm.Parameters.AddWithValue("OpenDate", (object)OpenDate??DBNull.Value);
            sqlComm.Parameters.AddWithValue("LastLoginTime", (object)LastLoginTime??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Status", (object)Status??DBNull.Value);
            sqlComm.Parameters.AddWithValue("ExperienceYear", (object)ExperienceYear??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Introduction", (object)Introduction??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Rank", (object)Rank??DBNull.Value);
            sqlComm.Parameters.AddWithValue("LikeVote", (object)LikeVote??DBNull.Value);
            sqlComm.Parameters.AddWithValue("DislikeVote", (object)DislikeVote??DBNull.Value);
            sqlComm.Parameters.AddWithValue("HeadPhoto", (object)HeadPhoto??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO Photographer ( PhotographerId,Email,PhotographerName,Password,PayRate,Gender,Age,Phone,OpenDate,LastLoginTime,Status,ExperienceYear,Introduction,Rank,LikeVote,DislikeVote,HeadPhoto ) values (@PhotographerId,@Email,@PhotographerName,@Password,@PayRate,@Gender,@Age,@Phone,@OpenDate,@LastLoginTime,@Status,@ExperienceYear,@Introduction,@Rank,@LikeVote,@DislikeVote,@HeadPhoto)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("PhotographerId", (object)PhotographerId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Email", (object)Email??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotographerName", (object)PhotographerName??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Password", (object)Password??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PayRate", (object)PayRate??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Gender", (object)Gender??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Age", (object)Age??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Phone", (object)Phone??DBNull.Value);
            sqlComm.Parameters.AddWithValue("OpenDate", (object)OpenDate??DBNull.Value);
            sqlComm.Parameters.AddWithValue("LastLoginTime", (object)LastLoginTime??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Status", (object)Status??DBNull.Value);
            sqlComm.Parameters.AddWithValue("ExperienceYear", (object)ExperienceYear??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Introduction", (object)Introduction??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Rank", (object)Rank??DBNull.Value);
            sqlComm.Parameters.AddWithValue("LikeVote", (object)LikeVote??DBNull.Value);
            sqlComm.Parameters.AddWithValue("DislikeVote", (object)DislikeVote??DBNull.Value);
            sqlComm.Parameters.AddWithValue("HeadPhoto", (object)HeadPhoto??DBNull.Value);

            sqlComm.CommandText ="UPDATE Photographer SET PhotographerId = @PhotographerId, Email = @Email, PhotographerName = @PhotographerName, Password = @Password, PayRate = @PayRate, Gender = @Gender, Age = @Age, Phone = @Phone, OpenDate = @OpenDate, LastLoginTime = @LastLoginTime, Status = @Status, ExperienceYear = @ExperienceYear, Introduction = @Introduction, Rank = @Rank, LikeVote = @LikeVote, DislikeVote = @DislikeVote, HeadPhoto = @HeadPhoto where PhotographerId = @PhotographerId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM Photographer  where PhotographerId = @PhotographerId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<Photographer>().Where(e =>  e.PhotographerId == PhotographerId ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            Photographer curEntity = currentEntity as Photographer;

            if( curEntity.PhotographerId == PhotographerId )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class PhotographerAccount
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("PhotographerId", (object)PhotographerId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("TotalBalance", (object)TotalBalance??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Balance", (object)Balance??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PendingBalance", (object)PendingBalance??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO PhotographerAccount ( PhotographerId,TotalBalance,Balance,PendingBalance ) values (@PhotographerId,@TotalBalance,@Balance,@PendingBalance)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("PhotographerId", (object)PhotographerId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("TotalBalance", (object)TotalBalance??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Balance", (object)Balance??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PendingBalance", (object)PendingBalance??DBNull.Value);

            sqlComm.CommandText ="UPDATE PhotographerAccount SET PhotographerId = @PhotographerId, TotalBalance = @TotalBalance, Balance = @Balance, PendingBalance = @PendingBalance where PhotographerId = @PhotographerId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM PhotographerAccount  where PhotographerId = @PhotographerId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<PhotographerAccount>().Where(e =>  e.PhotographerId == PhotographerId ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            PhotographerAccount curEntity = currentEntity as PhotographerAccount;

            if( curEntity.PhotographerId == PhotographerId )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class TxnRecord
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("SerialNo", (object)SerialNo??DBNull.Value);
            sqlComm.Parameters.AddWithValue("TxnType", (object)TxnType??DBNull.Value);
            sqlComm.Parameters.AddWithValue("TxnContent", (object)TxnContent??DBNull.Value);
            sqlComm.Parameters.AddWithValue("TxnTime", (object)TxnTime??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO TxnRecord ( SerialNo,TxnType,TxnContent,TxnTime ) values (@SerialNo,@TxnType,@TxnContent,@TxnTime)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("SerialNo", (object)SerialNo??DBNull.Value);
            sqlComm.Parameters.AddWithValue("TxnType", (object)TxnType??DBNull.Value);
            sqlComm.Parameters.AddWithValue("TxnContent", (object)TxnContent??DBNull.Value);
            sqlComm.Parameters.AddWithValue("TxnTime", (object)TxnTime??DBNull.Value);

            sqlComm.CommandText ="UPDATE TxnRecord SET SerialNo = @SerialNo, TxnType = @TxnType, TxnContent = @TxnContent, TxnTime = @TxnTime where SerialNo = @SerialNo ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM TxnRecord  where SerialNo = @SerialNo ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<TxnRecord>().Where(e =>  e.SerialNo == SerialNo ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            TxnRecord curEntity = currentEntity as TxnRecord;

            if( curEntity.SerialNo == SerialNo )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class Account
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("AccountId", (object)AccountId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Balance", (object)Balance??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotographerPay", (object)PhotographerPay??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PendingPay", (object)PendingPay??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Expense", (object)Expense??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO Account ( AccountId,Balance,PhotographerPay,PendingPay,Expense ) values (@AccountId,@Balance,@PhotographerPay,@PendingPay,@Expense)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("AccountId", (object)AccountId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Balance", (object)Balance??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotographerPay", (object)PhotographerPay??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PendingPay", (object)PendingPay??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Expense", (object)Expense??DBNull.Value);

            sqlComm.CommandText ="UPDATE Account SET AccountId = @AccountId, Balance = @Balance, PhotographerPay = @PhotographerPay, PendingPay = @PendingPay, Expense = @Expense where AccountId = @AccountId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM Account  where AccountId = @AccountId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<Account>().Where(e =>  e.AccountId == AccountId ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            Account curEntity = currentEntity as Account;

            if( curEntity.AccountId == AccountId )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class ExpenseItem
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("ItemId", (object)ItemId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("ItemName", (object)ItemName??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Description", (object)Description??DBNull.Value);
            sqlComm.Parameters.AddWithValue("ExpenseDate", (object)ExpenseDate??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Amount", (object)Amount??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO ExpenseItem ( ItemId,ItemName,Description,ExpenseDate,Amount ) values (@ItemId,@ItemName,@Description,@ExpenseDate,@Amount)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("ItemId", (object)ItemId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("ItemName", (object)ItemName??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Description", (object)Description??DBNull.Value);
            sqlComm.Parameters.AddWithValue("ExpenseDate", (object)ExpenseDate??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Amount", (object)Amount??DBNull.Value);

            sqlComm.CommandText ="UPDATE ExpenseItem SET ItemId = @ItemId, ItemName = @ItemName, Description = @Description, ExpenseDate = @ExpenseDate, Amount = @Amount where ItemId = @ItemId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM ExpenseItem  where ItemId = @ItemId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<ExpenseItem>().Where(e =>  e.ItemId == ItemId ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            ExpenseItem curEntity = currentEntity as ExpenseItem;

            if( curEntity.ItemId == ItemId )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class PhotoType
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("PhotoTypeId", (object)PhotoTypeId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotoTypeName", (object)PhotoTypeName??DBNull.Value);
            sqlComm.Parameters.AddWithValue("SortOrder", (object)SortOrder??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO PhotoType ( PhotoTypeId,PhotoTypeName,SortOrder ) values (@PhotoTypeId,@PhotoTypeName,@SortOrder)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("PhotoTypeId", (object)PhotoTypeId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotoTypeName", (object)PhotoTypeName??DBNull.Value);
            sqlComm.Parameters.AddWithValue("SortOrder", (object)SortOrder??DBNull.Value);

            sqlComm.CommandText ="UPDATE PhotoType SET PhotoTypeId = @PhotoTypeId, PhotoTypeName = @PhotoTypeName, SortOrder = @SortOrder where PhotoTypeId = @PhotoTypeId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM PhotoType  where PhotoTypeId = @PhotoTypeId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<PhotoType>().Where(e =>  e.PhotoTypeId == PhotoTypeId ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            PhotoType curEntity = currentEntity as PhotoType;

            if( curEntity.PhotoTypeId == PhotoTypeId )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class Txn
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("SerialNo", (object)SerialNo??DBNull.Value);
            sqlComm.Parameters.AddWithValue("TxnTime", (object)TxnTime??DBNull.Value);
            sqlComm.Parameters.AddWithValue("TxnId", (object)TxnId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("UserId", (object)UserId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("CustomerId", (object)CustomerId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("ErrorNo", (object)ErrorNo??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO Txn ( SerialNo,TxnTime,TxnId,UserId,CustomerId,ErrorNo ) values (@SerialNo,@TxnTime,@TxnId,@UserId,@CustomerId,@ErrorNo)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("SerialNo", (object)SerialNo??DBNull.Value);
            sqlComm.Parameters.AddWithValue("TxnTime", (object)TxnTime??DBNull.Value);
            sqlComm.Parameters.AddWithValue("TxnId", (object)TxnId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("UserId", (object)UserId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("CustomerId", (object)CustomerId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("ErrorNo", (object)ErrorNo??DBNull.Value);

            sqlComm.CommandText ="UPDATE Txn SET SerialNo = @SerialNo, TxnTime = @TxnTime, TxnId = @TxnId, UserId = @UserId, CustomerId = @CustomerId, ErrorNo = @ErrorNo where SerialNo = @SerialNo ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM Txn  where SerialNo = @SerialNo ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<Txn>().Where(e =>  e.SerialNo == SerialNo ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            Txn curEntity = currentEntity as Txn;

            if( curEntity.SerialNo == SerialNo )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class Customer
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("CustomerId", (object)CustomerId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Email", (object)Email??DBNull.Value);
            sqlComm.Parameters.AddWithValue("CustomerName", (object)CustomerName??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Password", (object)Password??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Gender", (object)Gender??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Age", (object)Age??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Address", (object)Address??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Phone", (object)Phone??DBNull.Value);
            sqlComm.Parameters.AddWithValue("OpenDate", (object)OpenDate??DBNull.Value);
            sqlComm.Parameters.AddWithValue("LastLoginTime", (object)LastLoginTime??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Status", (object)Status??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO Customer ( CustomerId,Email,CustomerName,Password,Gender,Age,Address,Phone,OpenDate,LastLoginTime,Status ) values (@CustomerId,@Email,@CustomerName,@Password,@Gender,@Age,@Address,@Phone,@OpenDate,@LastLoginTime,@Status)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("CustomerId", (object)CustomerId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Email", (object)Email??DBNull.Value);
            sqlComm.Parameters.AddWithValue("CustomerName", (object)CustomerName??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Password", (object)Password??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Gender", (object)Gender??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Age", (object)Age??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Address", (object)Address??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Phone", (object)Phone??DBNull.Value);
            sqlComm.Parameters.AddWithValue("OpenDate", (object)OpenDate??DBNull.Value);
            sqlComm.Parameters.AddWithValue("LastLoginTime", (object)LastLoginTime??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Status", (object)Status??DBNull.Value);

            sqlComm.CommandText ="UPDATE Customer SET CustomerId = @CustomerId, Email = @Email, CustomerName = @CustomerName, Password = @Password, Gender = @Gender, Age = @Age, Address = @Address, Phone = @Phone, OpenDate = @OpenDate, LastLoginTime = @LastLoginTime, Status = @Status where CustomerId = @CustomerId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM Customer  where CustomerId = @CustomerId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<Customer>().Where(e =>  e.CustomerId == CustomerId ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            Customer curEntity = currentEntity as Customer;

            if( curEntity.CustomerId == CustomerId )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class Offer
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("OfferId", (object)OfferId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("OfferName", (object)OfferName??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Description", (object)Description??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Price", (object)Price??DBNull.Value);
            sqlComm.Parameters.AddWithValue("StartTime", (object)StartTime??DBNull.Value);
            sqlComm.Parameters.AddWithValue("EndTime", (object)EndTime??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Status", (object)Status??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotoTypeId", (object)PhotoTypeId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("NoServicer", (object)NoServicer??DBNull.Value);
            sqlComm.Parameters.AddWithValue("MaxPeople", (object)MaxPeople??DBNull.Value);
            sqlComm.Parameters.AddWithValue("NoRawPhoto", (object)NoRawPhoto??DBNull.Value);
            sqlComm.Parameters.AddWithValue("NoRetouchedPhoto", (object)NoRetouchedPhoto??DBNull.Value);
            sqlComm.Parameters.AddWithValue("NoMakeup", (object)NoMakeup??DBNull.Value);
            sqlComm.Parameters.AddWithValue("NoCostume", (object)NoCostume??DBNull.Value);
            sqlComm.Parameters.AddWithValue("NoVenue", (object)NoVenue??DBNull.Value);
            sqlComm.Parameters.AddWithValue("AdditionalRetouchPrice", (object)AdditionalRetouchPrice??DBNull.Value);
            sqlComm.Parameters.AddWithValue("DurationHour", (object)DurationHour??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Comment", (object)Comment??DBNull.Value);
            sqlComm.Parameters.AddWithValue("SortOrder", (object)SortOrder??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO Offer ( OfferId,OfferName,Description,Price,StartTime,EndTime,Status,PhotoTypeId,NoServicer,MaxPeople,NoRawPhoto,NoRetouchedPhoto,NoMakeup,NoCostume,NoVenue,AdditionalRetouchPrice,DurationHour,Comment,SortOrder ) values (@OfferId,@OfferName,@Description,@Price,@StartTime,@EndTime,@Status,@PhotoTypeId,@NoServicer,@MaxPeople,@NoRawPhoto,@NoRetouchedPhoto,@NoMakeup,@NoCostume,@NoVenue,@AdditionalRetouchPrice,@DurationHour,@Comment,@SortOrder)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("OfferId", (object)OfferId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("OfferName", (object)OfferName??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Description", (object)Description??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Price", (object)Price??DBNull.Value);
            sqlComm.Parameters.AddWithValue("StartTime", (object)StartTime??DBNull.Value);
            sqlComm.Parameters.AddWithValue("EndTime", (object)EndTime??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Status", (object)Status??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotoTypeId", (object)PhotoTypeId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("NoServicer", (object)NoServicer??DBNull.Value);
            sqlComm.Parameters.AddWithValue("MaxPeople", (object)MaxPeople??DBNull.Value);
            sqlComm.Parameters.AddWithValue("NoRawPhoto", (object)NoRawPhoto??DBNull.Value);
            sqlComm.Parameters.AddWithValue("NoRetouchedPhoto", (object)NoRetouchedPhoto??DBNull.Value);
            sqlComm.Parameters.AddWithValue("NoMakeup", (object)NoMakeup??DBNull.Value);
            sqlComm.Parameters.AddWithValue("NoCostume", (object)NoCostume??DBNull.Value);
            sqlComm.Parameters.AddWithValue("NoVenue", (object)NoVenue??DBNull.Value);
            sqlComm.Parameters.AddWithValue("AdditionalRetouchPrice", (object)AdditionalRetouchPrice??DBNull.Value);
            sqlComm.Parameters.AddWithValue("DurationHour", (object)DurationHour??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Comment", (object)Comment??DBNull.Value);
            sqlComm.Parameters.AddWithValue("SortOrder", (object)SortOrder??DBNull.Value);

            sqlComm.CommandText ="UPDATE Offer SET OfferId = @OfferId, OfferName = @OfferName, Description = @Description, Price = @Price, StartTime = @StartTime, EndTime = @EndTime, Status = @Status, PhotoTypeId = @PhotoTypeId, NoServicer = @NoServicer, MaxPeople = @MaxPeople, NoRawPhoto = @NoRawPhoto, NoRetouchedPhoto = @NoRetouchedPhoto, NoMakeup = @NoMakeup, NoCostume = @NoCostume, NoVenue = @NoVenue, AdditionalRetouchPrice = @AdditionalRetouchPrice, DurationHour = @DurationHour, Comment = @Comment, SortOrder = @SortOrder where OfferId = @OfferId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM Offer  where OfferId = @OfferId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<Offer>().Where(e =>  e.OfferId == OfferId ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            Offer curEntity = currentEntity as Offer;

            if( curEntity.OfferId == OfferId )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class OfferPhotographer
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("OfferId", (object)OfferId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotographerId", (object)PhotographerId??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO OfferPhotographer ( OfferId,PhotographerId ) values (@OfferId,@PhotographerId)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("OfferId", (object)OfferId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotographerId", (object)PhotographerId??DBNull.Value);

            sqlComm.CommandText ="UPDATE OfferPhotographer SET OfferId = @OfferId, PhotographerId = @PhotographerId where OfferId = @OfferId and PhotographerId = @PhotographerId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM OfferPhotographer  where OfferId = @OfferId and PhotographerId = @PhotographerId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<OfferPhotographer>().Where(e =>  e.OfferId == OfferId && e.PhotographerId == PhotographerId ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            OfferPhotographer curEntity = currentEntity as OfferPhotographer;

            if( curEntity.OfferId == OfferId && curEntity.PhotographerId == PhotographerId )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class PhotographerWork
    {
        public override bool InsertSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("PhotographerWorkId", (object)PhotographerWorkId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotographerId", (object)PhotographerId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotoTypeId", (object)PhotoTypeId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Name", (object)Name??DBNull.Value);
            sqlComm.Parameters.AddWithValue("SortOrder", (object)SortOrder??DBNull.Value);

            sqlComm.CommandText ="INSERT INTO PhotographerWork ( PhotographerWorkId,PhotographerId,PhotoTypeId,Name,SortOrder ) values (@PhotographerWorkId,@PhotographerId,@PhotoTypeId,@Name,@SortOrder)";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool UpdateSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 
            sqlComm.Parameters.AddWithValue("PhotographerWorkId", (object)PhotographerWorkId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotographerId", (object)PhotographerId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("PhotoTypeId", (object)PhotoTypeId??DBNull.Value);
            sqlComm.Parameters.AddWithValue("Name", (object)Name??DBNull.Value);
            sqlComm.Parameters.AddWithValue("SortOrder", (object)SortOrder??DBNull.Value);

            sqlComm.CommandText ="UPDATE PhotographerWork SET PhotographerWorkId = @PhotographerWorkId, PhotographerId = @PhotographerId, PhotoTypeId = @PhotoTypeId, Name = @Name, SortOrder = @SortOrder where PhotographerWorkId = @PhotographerWorkId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override bool DeleteSql()
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand(); 

            sqlComm.CommandText ="DELETE FROM PhotographerWork  where PhotographerWorkId = @PhotographerWorkId ";

            int rows = 0;
            try
            {
                sqlConn.Open();
                rows = sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            if(rows >0)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public override Entity Fetch()
        {
            using (var dc = new HostDBDataContext())
            {
                return dc.GetTable<PhotographerWork>().Where(e =>  e.PhotographerWorkId == PhotographerWorkId ).FirstOrDefault();
            }
        }

        public override bool Compare(Entity currentEntity)
        {
            PhotographerWork curEntity = currentEntity as PhotographerWork;

            if( curEntity.PhotographerWorkId == PhotographerWorkId )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
