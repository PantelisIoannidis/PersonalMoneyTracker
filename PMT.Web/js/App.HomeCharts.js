var homeCharts = function () {

    var incomeByCategoryChartData = [];
    var expenseByCategoryChartData = [];
    var incomeVsExpenseChartData = [];
    var chartFontColor = '#eee';

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
        Chart.defaults.global.defaultFontColor = chartFontColor;
        var ctx = document.getElementById("incomeVsExpenseChart");
        var myChart = new Chart(ctx, {
            type: 'horizontalBar',
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
                    }],
                    xAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }],
                }
            }
        });
    }

    function DrawExpenseByCategoryChart() {
        Chart.defaults.global.defaultFontColor = chartFontColor;
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
        Chart.defaults.global.defaultFontColor = chartFontColor;
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

    function GETproperty(classOrId, property) {
        var FirstChar = classOrId.charAt(0); var Remaining = classOrId.substring(1);
        var elem = (FirstChar == '#') ? document.getElementById(Remaining) : document.getElementsByClassName(Remaining)[0];
        return window.getComputedStyle(elem, null).getPropertyValue(property);
    }

    function onHomeChartsInit() {
        chartFontColor = GETproperty('#chartFontColor', 'color');
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