var homeCharts = function () {

    var incomeVsExpenseChartData = [];
    function setIncomeVsExpenseChartData(s) {
        incomeVsExpenseChartData.push(JSON.parse(s));
    };

    function onHomeChartsInit() {
        //console.log(incomeVsExpenseChartData[0]);
        var ctx = document.getElementById("incomeVsExpenseChart");
        var myChart = new Chart(ctx, {
            type: 'bar',
            data:  incomeVsExpenseChartData[0] ,
            options: {
                title: {
                    display: false,
                    text: 'Income vs Expense'
                },
                legend: {
                    display: false
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });
    };

    return {
        onHomeChartsInit: onHomeChartsInit,
        setIncomeVsExpenseChartData: setIncomeVsExpenseChartData
    };
}();