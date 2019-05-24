# nodejs_iisnode_setup

## installs:
[IIS v10 64bit URL REWRITE module](https://www.microsoft.com/en-us/download/details.aspx?id=47337)

[NodeJS v10.x](https://nodejs.org/en/)

[IISNODE](https://github.com/azure/iisnode/wiki/iisnode-releases)

## Process
* Install url rewrite
* Install node JS
* Install IISNODE
* Give IIS_IUSRS permissions to: C:\Program Files\iisnode\www
* Run setupsamples.bat from C:\Program Files\iisnode
* Read what it says before hitting ok
* Test http://localhost/node <- especially URL rewrite

## setup new site:
* Create site as usual
* Add iisnode folder to website home dir. Ex: C:\inetpub\websites\test-poc\iisnode
* Give web site apppool permissions to iisnode folder (IIS AppPool\apppoolname) (remember to set location to server)
* Open web site folder in cmd and type npm install express

### site files to test iisnode works:
[server.js](https://github.com/Sidstling/nodejs_iisnode_setup/blob/master/server.js)

[web.config](https://github.com/Sidstling/nodejs_iisnode_setup/blob/master/web.config)

Go to url ex: https://site.local and see if you're met by:
"Express is working on IISnode!"
