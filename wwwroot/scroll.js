function scroll(index) {
    const _grid = document.getElementsByClassName("e-grid")[0].blazor__instance;
    const _rowHeight = 45;
    _grid.getContent().scrollTo({top: (index - 1) * _rowHeight, left: 0, behavior: "smooth"});
}

function alert(message) {
    alert(message);
}

var dotnetInstance;

function detail(dotnet) {
    dotnetInstance = dotnet; // dotnet instance to invoke C# method from JS  
}

document.addEventListener("click", function (args) {
    if (args.target.classList.contains("e-dtdiagonaldown") || args.target.classList.contains("e-detailrowexpand")) {
        dotnetInstance.invokeMethodAsync("DetailCollapse"); // call C# method from javascript function 
    }
});