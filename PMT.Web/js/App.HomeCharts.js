var homeCharts = function () {

    var incomeByCategoryChartData = [];
    var expenseByCategoryChartData = [];
    var incomeVsExpenseChartData = [];

    function setIncomeVsExpenseChartData(s) {
        incomeVsExpenseChartData.push(JSON.parse(s));
    };
    function setIncomeByCategoryChartData(s) {
        incomeByCategoryChartData.push(JSON.parse(s));
    };
    function setExpenseByCategoryChartData(s) {
        expenseByCategoryChartData.push(JSON.parse(s));
    };

    function DrawIncomeVsExpenseChart() {
        var ctx = document.getElementById("incomeVsExpenseChart");
        var myChart = new Chart(ctx, {
            type: 'bar',
            data: incomeVsExpenseChartData[0],
            options: {
                title: {
                    display: true,
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
    }

    function DrawExpenseByCategoryChart() {
        var ctx = document.getElementById("expenseByCategoryChart");
        var myChart = new Chart(ctx, {
            type: 'pie',
            data: expenseByCategoryChartData[0],
            options: {
                title: {
                    display: true,
                    text: 'Expense by Category'
                },
                legend: {
                    display: true
                },
                
            }
        });
    }

    function DrawIncomeByCategoryChart() {
        var ctx = document.getElementById("incomeByCategoryChart");
        var myChart = new Chart(ctx, {
            type: 'pie',
            data: incomeByCategoryChartData[0],
            options: {
                title: {
                    display: true,
                    text: 'Income by Category'
                },
                legend: {
                    display: true
                },

            }
        });
    }



    function onHomeChartsInit() {
        DrawIncomeVsExpenseChart();
        DrawExpenseByCategoryChart();
        DrawIncomeByCategoryChart();
    };

    return {
        onHomeChartsInit: onHomeChartsInit,
        setIncomeVsExpenseChartData: setIncomeVsExpenseChartData,
        setIncomeByCategoryChartData: setIncomeByCategoryChartData,
        setExpenseByCategoryChartData: setExpenseByCategoryChartData,

    };
}();