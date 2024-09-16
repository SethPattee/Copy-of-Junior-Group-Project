##Files of Interest
- kube/telemtry: These files are responsible for running Kubernetes. There are logs shown by Grafana (using zipkin, open telemtry, blackbox exporter, loki, and prometheus) and files for deploying the project to a server (using Nginx) or azure
- AutoShopMoble: This is the MAUI mobile app that uses shared components from the AutoShopAppLibary. All the shared components were optimized for mobile as well as web.
- AutoShopTDDSuit: This houses our tests that consist of PlayWright, NUnit, and XUnit. Some are unfinished or commented out because this is not the final build of the project.
- AutoEmailFunction: This is an Azure Functions that we implemented. What it does is take the information that the user inputs, creates a custom pdf, and emails the pdf to the product owner and customer. Other Azure Tools that we used were Azure Postgres, Azure Maps, and Azure Web Hosting
