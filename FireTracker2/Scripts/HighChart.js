

<script type="text/javascript">
    $(function () {
        $.post('@Url.Action("GetHardCodedHighBarData","Charts")').then(function (response) {
            new HighChart.Pie({
                element: 'HardCodedHighChart',
                data: response,
                xkey: 'label',
                ykeys: ['value'],
                labels: ['Ticket Count'],
                resize: true


            })
        })
    })
    </script>
    <script type="text/javascript">
        $(function () {
            $.post('@Url.Action("GetHardCodedHighBarData","Charts")').then(function (response) {
                new Morris.Bar({
                    element: 'HardCodedMorrisChart',
                    data: response,
                    xkey: 'label',
                    ykeys: ['value'],
                    labels: ['Ticket Count'],
                    resize: true


                })
            })
        })
    </script>
    <script type="text/javascript">
        @*$(function () {
            $.post('@Url.Action("GetHighPieData","HighChart")').then(function (response) {
                new Highcharts.chart('highchart', {

                    data: response,
                    chart: {
                        type: 'pie',
                        options3d: {
                            enabled: true,
                            alpha: 45,
                            beta: 0
                        }
                    },
                    title: {
                        text: 'Number of tickets and status'
                    },
                    tooltip: {
                        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                    },
                    plotOptions: {
                        pie: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            depth: 35,
                            dataLabels: {
                                enabled: true,
                                format: '{point.name}'
                            }
                        }
                    },
                    xAxis: {
                        title: { 'label'},
                    },
                    yAxis: ['value'],
                    xkeys: 'label',
                    ykeys: ['value'],
                    labels: ['Ticket Count'],
                    series: [{
                        type: 'pie',
                        allowPointSelect: true,
                        name: 'Tickets Count',
                        data: GetHighPieData(data)
                    }]
                })
            })
        });*@
    </script>
    <script>
        buildTicketStatusChart();
        function buildTicketStatusChart() {
            AjaxHighChart('@Url.Action("GetHighPieData","HighChart")');
        }
     function AjaxHighChart(dataSet){
            $.ajax({
                url: dataSet,
                success: function (response) {
                    HighChartStatus(response);
                }
            });
        }
    </script>
    <script>
        function TicketStatusDataPie(data) {
        var result = [];
            $.each(data, function (index, item) {
                var ticketstatusPie = [item.label, Number(item.value)];
result.push(ticketstatusPie);
});
return result;
}
    </script>
    <script>
        //Highcharts for Ticket Priorities
        function HighChartStatus(data) {
            Highcharts.chart('highchart', {
                colors: ['#ff0000', '#ff9900', '#50B432', '#DDDF00', '#058DC7'],
                chart: {
                    type: 'pie',
                    options3d: {
                        enabled: true,
                        alpha: 45,
                        beta: 0
                    },
                    shadow: true,
                    backgroundColor: {
                        linearGradient: { x1: 0, y1: 0, x2: 1, y2: 1 },
                        stops: [
                            [0, 'rgb(255, 255, 255)'],
                            [1, 'rgb(240, 240, 255)']
                        ]
                    }
                },
                title: {
                    text: 'Ticket Status Counts'
                },
                subtitle: {
                    text: 'Immediate, High, Medium, Low, None'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        depth: 45,
                        dataLabels: {
                            enabled: true,
                            format: '{point.name}'
                        },
                        showInLegend: true
                    }
                },
                series: [{
                    type: 'pie',
                    allowPointSelect: true,
                    name: 'Tickets Count',
                    data: TicketStatusDataPie(data),
                }],

            });
        }


    </script>
