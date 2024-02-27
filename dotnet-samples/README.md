# .NET App Samples

This sample shows how to deploy and manage app with .NET in Azure Spring Apps.

## Prerequisite

* [Azure CLI](https://docs.microsoft.com/cli/azure/install-azure-cli?view=azure-cli-latest) or [Azure Cloud Shell](https://docs.microsoft.com/azure/cloud-shell/overview)

## How to run

1. Clone this repo and go to folder

    ```bash
    git clone https://github.com/Azure-Samples/azure-spring-apps-samples
    cd azure-spring-apps-samples/dotnet-samples
    ```

1. Install Azure CLI extension for Azure Spring Apps

    ```bash
    az extension add --name spring
    ```


## Provision your Azure Spring Apps instance

Please reference doc to provision Azure Spring Apps instance: https://learn.microsoft.com/azure/spring-apps/quickstart?pivots=sc-standard

Create environment variables file `setup-env-variables.sh` based on template. 
```bash
cp setup-env-variables-template.sh setup-env-variables.sh
```

Update below resource information in `setup-env-variables.sh`.
```bash
export SUBSCRIPTION='subscription-id'                 # replace it with your subscription-id
export RESOURCE_GROUP='resource-group-name'           # existing resource group or one that will be created in next steps
export SPRING_APPS_SERVICE='azure-spring-apps-name'   # name of the service that will be created in the next steps
```

Source setting.
```bash
source ./setup-env-variables.sh
```

Update default subscription.
```bash
az account set --subscription ${SUBSCRIPTION}
```

### Deploy app instance
```bash
az spring app create -g ${RESOURCE_GROUP} -s ${SPRING_APPS_SERVICE} -n weatherforecast1 

az spring app deploy -g ${RESOURCE_GROUP} -s ${SPRING_APPS_SERVICE} -n weatherforecast1 --source-path --apm newrelic
```
