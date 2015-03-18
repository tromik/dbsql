using System;
using System.Data.SqlClient;

namespace dbsql
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            int idVal = 0;
            string nameVal = "";
            string dateVal = "";
            string catVal = "";
            double amntVal = 0;
            string sqlRec = "";
            int cnt = 1;

            SqlConnection mssql = new SqlConnection("server=localhost;" +
                "Trusted_Connection=yes;" +
                "database=training; " +
                "connection timeout=30;" +
                "MultipleActiveResultSets=true");

            try
            {
                mssql.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            SqlCommand myDelete = new SqlCommand("DELETE FROM expenses_stage", mssql);
            myDelete.ExecuteNonQuery();

            try
            {
                SqlDataReader sqlReader = null;
                SqlCommand selectExp = new SqlCommand("SELECT * FROM expenses_source ORDER BY ID", mssql);
                sqlReader = selectExp.ExecuteReader();

                while (sqlReader.Read())
                {
                    idVal = Convert.ToInt32(sqlReader["ID"]);
                    nameVal = sqlReader["Name"].ToString().Trim();
                    dateVal = sqlReader["Date"].ToString().Trim();
                    catVal = sqlReader["Category"].ToString().Trim();
                    amntVal = Convert.ToInt32(sqlReader["Amount"]);

                    sqlRec = "==> " + idVal + " | " + nameVal + " | " + dateVal + " | " + catVal + " | " + amntVal;

                    if (idVal == 0)
                    {
                        Console.Write("ID is NULL : ");
                        Console.WriteLine(sqlRec);
                    }
                    else if (nameVal == "")
                    {
                        Console.Write("Name is NULL : ");
                        Console.WriteLine(sqlRec);
                    }
                    else if (dateVal == "")
                    {
                        Console.Write("Date is NULL : ");
                        Console.WriteLine(sqlRec);
                    }
                    else if (catVal == "")
                    {
                        Console.Write("Category is NULL : ");
                        Console.WriteLine(sqlRec);
                    }
                    else if (amntVal == 0)
                    {
                        Console.Write("Amount is NULL : ");
                        Console.WriteLine(sqlRec);
                    }
                    else
                        if (catVal == "Food" && amntVal > 200)
                        {
                            Console.Write("Food expense is too high : ");
                            Console.WriteLine(sqlRec);
                        }
                        else
                        {
                            Console.Write("Good record : ");
                            Console.WriteLine(sqlRec);

                            Console.WriteLine(" ====> AER FIRE--Inserting record!");

                            for (cnt = 1; cnt <= 2; cnt++)
                            {
                                if (cnt == 1)
                                {
                                    SqlCommand insertExp = new SqlCommand("INSERT INTO expenses_stage (ID, Name, Date, Category, Amount) " +
                                        "VALUES (" + idVal + ", '" + nameVal + "', '" + dateVal + "', '" + catVal + "', " + amntVal + ")", mssql);

                                    insertExp.ExecuteNonQuery();
                                }
                                else
                                {
                                    SqlCommand insertExp = new SqlCommand("INSERT INTO expenses_stage (ID, Name, Date, Category, Amount) " +
                                        "VALUES (" + idVal + ", '" + nameVal + "', '" + dateVal + "', 'AP', " + amntVal + ")", mssql);

                                    insertExp.ExecuteNonQuery();
                                }
                            }
                        }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadKey();
            }

            try
            {
                mssql.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadKey();
            }

            Console.ReadKey();
        }
    }
}