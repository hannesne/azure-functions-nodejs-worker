[![Build status](https://ci.appveyor.com/api/projects/status/inqsg64h792agrji?svg=true)](https://ci.appveyor.com/project/appsvc/azure-functions-nodejs-worker/branch/dev)

## Getting Started

[Azure Functions Language Extensibility Wiki](https://github.com/Azure/azure-webjobs-sdk-script/wiki/Language-Extensibility)
[worker.config.json](https://github.com/Azure/azure-functions-host/wiki/Authoring-&-Testing-Language-Extensions#workerconfigjson)

- `git clone https://github.com/Azure/azure-functions-nodejs-worker`
- `cd azure-functions-nodejs-worker`
- `npm install`
- `npm run build`
  - generate protobuf definitions & run typescript compiler
- `npm test`
- `$env:WORKERS__NODE__PATH = <path-to-nodejsWorker.js>`
  - configure the functions host to use the development version of the worker

## Debugging

Note that changing the environment variable might not actually work. If it doesn't, start your functions host up, and check the output for the path to the nodejsworker that it's currently using. Example: `Starting language worker process:node  "%userprofile%\AppData\Roaming\npm\node_modules\azure-functions-core-tools\bin\workers\node\dist/src/nodejsWorker.js" --host 127.0.0.1 --port 5134 --workerId fd9b17c3-8ffb-49f7-a4e3-089a780e7a00 --requestId 14e27374-9395-42d4-a639-bd67e0e770a4 --grpcMaxMessageLength 134217728`

Next navigate to the node folder in that path, in this case `%userprofile%\AppData\Roaming\npm\node_modules\azure-functions-core-tools\bin\workers\node`, and edit the `worker.config.json` file. Change the value of the defaultworkerpath value to the absolute path of your `<currentworkingdirectory>/dist/src/nodejsworker.js` file, produced by the node build. Example file content:

```json
{
    "description":{
        "language":"node",
        "extensions":[".js"],
        "defaultExecutablePath":"node",
        "defaultWorkerPath":"C:/dev/azure-functions-nodejs-worker/dist/src/nodejsWorker.js"
    }
}
```

Now restart you function host. The output should now show that it's using the nodejsworker.js file at the location you specified. Attach vscode to the node process, by picking the _Attach by Process ID_ option from the debug configuration list. When you now launch the debugger, while the function host is running a node function, you'll see a list of Node processes. One of these points to your nodejsWorker.js file. Pick this process. Any breakpoints you now set in your js files will be hit. If the map files were generated by the `npm run build` command, you can also place breakpoints in the ts files. The map files will be in the same directory as the js files.

## Publishing

`package.ps1` creates the nuget package for the worker.

It builds and webpacks the generated node files into a bundle.
We include several grpc native modules, for x86/x64 versions of windows, osx, linux

The nuget package can be deployed from the appveyor job at: https://ci.appveyor.com/project/appsvc/azure-functions-nodejs-worker

## Contributing

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
