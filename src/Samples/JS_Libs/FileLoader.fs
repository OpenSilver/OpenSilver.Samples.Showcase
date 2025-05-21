namespace OpenSilver.Samples.Showcase

open System
open System.Threading.Tasks
open OpenSilver

module FileLoader =
    let TryLoadJavaScriptFile (url: string) : Task<bool> =
        task {
            try
                let! _ = Interop.LoadJavaScriptFile(url)
                return true
            with
            | ex ->
                Console.WriteLine(ex)
                return false
        }
        
    let TryLoadCssFile (url: string) : Task<bool> =
        task {
            try
                let! _ = Interop.LoadCssFile(url)
                return true
            with
            | ex ->
                Console.WriteLine(ex)
                return false
        }
