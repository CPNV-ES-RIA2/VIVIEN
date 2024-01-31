# RIA2
The project repository for the RIA2 module.

## Description
This is the architecture of the project.
```mermaid
flowchart LR
    subgraph FrontEnd[Frontend]
        subgraph WebClient["Web Client (MVVM)"]
            direction TB
            WebClientView(Views) <--> WebClientViewModel(View Models)
            WebClientViewModel <--> WebClientModels(Models)
            WebClientModels <--> WebClientRouter(Router)
            WebClientRouter <--> WebClientHttpClient(HTTP client)
        end
    end
    subgraph BackEnd[Backend]
        subgraph LabelDetector["Label Detector (MVC)"]
            direction TB
            LabelDetectorWebAPI(ASP.NET Web API) --> LabelDetectorControllers(Controllers)
            LabelDetectorControllers --> LabelDetectorModels(Models)
            LabelDetectorModels --> LabelDetectorDataModels(Data Models)
        end
        subgraph DataObject["Data Object (MVC)"]
            direction TB
            DataObjectWebAPI(ASP.NET Web API) --> DataObjectControllers(Controllers)
            DataObjectControllers --> DataObjectModels(Models)
            DataObjectModels --> DataObjectDataModels(Data Models)
        end
        subgraph Gateway["API Gateway (MVC)"]
            direction TB
            APIGatewayWebAPI(ASP.NET Web API) --> APIGatewayControllers(Controllers)
            APIGatewayControllers --> APIGatewayModels(Models)
        end
    end
    LabelDetector & DataObject ---|REST| Gateway
    Gateway <-->|REST| WebClient
```

## Getting Started
TODO

### Prerequisites
List all dependencies and their version needed by the project as :

* IDE: 
* Package manager: 
* OS: 
* Runtime: 

### Configuration
TODO

## Deployment
TODO

### On integration environment
TODO

## Directory structure
TODO

## Collaborate
TODO

## License
TODO

## Contact

To get in contact with the repo maintainer, send an email to Vivien.Piccin@eduvaud.ch or head to [issues](https://github.com/VivienCPNV/RIA2/issues) if you'd like to report something.
