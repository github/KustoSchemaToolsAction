# KustoSchemaToolsAction

This tool can be used to synchronized your schema from Azure Data Explorer (Kusto) cluster to yml files in a GitHub repository and back, using GitHub Actions.

## Getting Started

1. Create a repository for versioning your files
1. Copy the [actions.yaml](workflows/action.yaml) file from the workflows folder to your repository in the folder `.github/actions/KustoSchemaTools`
1. Copy the workflows that you want ([import](workflows/import.yml), [diff](workflows/diff.yml), [apply](workflows/apply.yml)) from the workflows folder to your repository in the folder `.github/workflows`
1. The workflows use OIDC to login to Azure. You can use a service principal instead. Make sure, that the identity you use has access to your Azure Data Explorer (Kusto) cluster
1. Create a folder for your cluster/deployment and add a [clusters](schema/clusters.yml) file.
1. Import the databases you want to synchronize
1. Make changes to the yaml files and create a PR
1. The diffs will be posted as PR comment
1. Merge the PR to roll out the changes to your Azure Data Explorer (Kusto) cluster

This project can be used as it is and doesn't require customizations. In the repo 

## Adding custom behavior

KustoSchemaTools uses plugins for reading the and writing the schema from or to a database or yaml file. You can find more docs on that in the [KustoSchemaTools repo](https://github.com/github/KustoSchemaTools). 

The easiest way to add your own logic is to 
1. [Fork](fork) this repository 
1. Implement your own plugins ([examle](plugins/TableGroupPlugin.cs))
1. Register them in the [Program.cs](https://github.com/github/KustoSchemaToolsAction/blob/main/KustoSchemaCLI/Program.cs#L15)
1. Build your own docker container ([workflow](.github/workflows/publish_image.yml))
1. Add the definition of your [action](action/action.yaml) if required.
1. Use the provided workflows with your container ([import](workflows/import.yml), [diff](workflows/diff.yml), [apply](workflows/apply.yml))