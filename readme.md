# Captain Hook

## Description

An experiment to see how difficult it was to create a pastebin style app that filled a specific purpose, that being testing a webhook. This will not support parallel posts and gets in it's current state.

## Installation

You will need to setup an Azure Function, a Service Connecter to a Blob store and a  Managed Identity in your Azure subscription. The identity will need to be given rights to deploy to the function (I chose Website Contributor against the Function). The `main_thecaptainhook.yaml` file was previously setup for a publishing profile, but I wanted to use Managed Identities to deploy. You will need to setup the folloing secrets in your github action secrets area if you want this action workflow to run:

- AZURE_CLIENT_ID 
- AZURE_TENANT_ID
- AZURE_SUBSCRIPTION_ID


## Usage
You need to first make a POST request with a body to the `api/PostToMe`.
You can then make a GET request to `api/ReadAndClear` which will return what you posted and also clear it, i.e. if you make 2 GET calls back to back, the second one will be empty.
