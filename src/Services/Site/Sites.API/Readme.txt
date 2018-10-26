1. 下载并安装.NET Core 2.0.5
    https://github.com/dotnet/core/blob/master/release-notes/download-archives/2.0.5-download.md

2. 安装MySql

3. 使用visual studio 2017
    打开根目录下的eShopOnContainers-ServicesAndWebApps.sln

4. 使用dotnet ef更新数据库
     a) dotnet ef migrations add addClientTable --context SitesContext
     b) dotnet ef database update

5. 项目运行时会自动创建数据库

6. 使用swagger ui测试api
    http://localhost:5000/swagger/

7. Clients Api (POST / PUT / GET)
    Client Model:
    /// Id (Guid)
    /// First name
    /// Last name
    /// Middle name
    /// Email address
    /// Mobile phone
    /// Home phone
    /// Work phone
    /// Address
    /// Address line 2
    /// City
    /// StateProvince
    /// Country
    /// Postal code
    /// Zip code
    /// Gender
    /// Birth date
    /// The date of the first appointment
    /// Client notes
    /// URL for the client's image in the profile
