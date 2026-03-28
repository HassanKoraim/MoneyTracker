import { Component, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { Chart } from 'chart.js/auto';
import { Transactions } from '../../services/transactions';

@Component({
  selector: 'app-income-outcome',
  templateUrl: './income-outcome.html',
  styleUrl: './income-outcome.scss',
})
export class IncomeOutcome implements AfterViewInit {

  Income: number = 0;
  Outcome: number = 0;

  @ViewChild('donutCanvas') chartRef!: ElementRef;
  chart: any;

  constructor(private transactionsService: Transactions) {}

  ngAfterViewInit() {
    this.createChart(); // create empty chart first
    this.loadData();    // then load API data
  }

  // ✅ Load API data
  loadData() {
    this.transactionsService.GetAmount('Income').subscribe((data: any) => {
      this.Income = data;
      this.updateChart();
    });

    this.transactionsService.GetAmount('Expense').subscribe((data: any) => {
      this.Outcome = data;
      this.updateChart();
    });
  }

  // ✅ Create chart
  createChart() {
    if (this.chart) {
      this.chart.destroy();
    }

    const ctx = this.chartRef.nativeElement.getContext('2d'); // 🔥 FIX

    this.chart = new Chart(ctx, {
      type: 'doughnut',
      data: {
        labels: ['Income', 'OutCome'],
        datasets: [{
          data: [0, 0], // start empty
          backgroundColor: ['#2a9d8f', '#e76f51'],
          hoverOffset: 20
        }]
      },
      options: {
  responsive: true,
  animation: {
    animateScale: true, 
    animateRotate: true,
    duration: 1000
  },
  plugins: {
    legend: {
      display: false
    }
  }
},
plugins: [
  {
    id: 'advancedLabels',
    afterDraw(chart) {
      const { ctx } = chart;
      const dataset = chart.data.datasets[0];
      const meta = chart.getDatasetMeta(0);

      const data = dataset.data as number[];
      const total = data.reduce((a, b) => a + b, 0);

      ctx.save();

      // 🔹 Draw labels inside slices
      meta.data.forEach((element, index) => {
        const position = element.tooltipPosition(true);
        const label = chart.data.labels?.[index] ?? '';
        const value = data[index];
        const percentage = total ? ((value / total) * 100).toFixed(0) : 0;

        if (position && position.x != null && position.y != null) {
          ctx.fillStyle = '#fff';
          ctx.font = 'bold 15px Arial';
          ctx.textAlign = 'center';

          // Line 1 → Label
          ctx.fillText(label as string, position.x, position.y - 10);

          // Line 2 → Value
       //   ctx.fillText(value.toString(), position.x, position.y + 5);  // Income and OutCome number

          // Line 3 → Percentage
          ctx.font = '15px Arial';
          ctx.fillText(`${percentage}%`, position.x, position.y + 20);
        }
      });

      // 🔹 Draw center total
      const centerX = chart.width / 2;
      const centerY = chart.height / 2;

      ctx.fillStyle = '#333';
      ctx.font = 'bold 18px Arial';
      ctx.textAlign = 'center';
      ctx.fillText('Total', centerX, centerY - 10);

      ctx.font = 'bold 20px Arial';
      ctx.fillText(total.toFixed(2).toString(), centerX, centerY + 15);

      ctx.restore();
    }
  }
]
    });
  }

  // ✅ Update chart when data arrives
  updateChart() {
    if (!this.chart) return;

    this.chart.data.datasets[0].data = [this.Income, this.Outcome];
    this.chart.update();
  }
}





// First Way
// import { Component, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
// import { Chart } from 'chart.js/auto';
// import { Transactions } from '../services/transactions';

// @Component({
//   selector: 'app-income-outcome',
//   templateUrl: './income-outcome.html',
//   styleUrl: './income-outcome.scss',
// })
// export class IncomeOutcome implements AfterViewInit {
//   Income:any= 0;
//   Outcome:any= 0;
//   constructor(private transactionsService:Transactions ){}
//     ngOnInit(){
//       this.transactionsService.GetAmount('Income').subscribe((data:any)=>{
//       //  transactionDate: data.transactionDate ? new Date(data.transactionDate) : null
//        console.log(data);
//        this.Income = data;
       
//       })
//        this.transactionsService.GetAmount('Expense').subscribe((data:any)=>{
//       //  transactionDate: data.transactionDate ? new Date(data.transactionDate) : null
//        console.log(data);
//        this.Outcome = data;
//       })
//     //  this.UpdateChart(this.Income,this.Outcome);
//     }
//   @ViewChild('donutCanvas') chartRef!: ElementRef;
//   chart: any;   // store chart instance

//   ngAfterViewInit() {
//     this.createChart();
//   }

//   createChart() {
//     // 🔥 Destroy old chart if exists
//     if (this.chart) {
//       this.chart.destroy();
//     }

//     // const ctx = this.chartRef.nativeElement.getContext('2d');

//     // this.chart = new Chart(ctx, {
//     //   type: 'doughnut',
//     //   data: {
//     //     labels: ['Income', 'OutCome'],
//     //     datasets: [{
//     //       data: [this.Income, this.Outcome],
//     //       backgroundColor: [
//     //         '#2a9d8f',
//     //          '#e76f51'
//     //       ],
//     //       hoverOffset: 20
//     //     }]
//     //   },
//     //   options: {
//     //     responsive: true,
//     //     plugins: {
//     //       legend: {
//     //         position: 'top',
//     //       }
//     //     }//,cutout: '70%'   // increase = bigger hole
//     //   }
//     // });
    
//   }
//   UpdateChart(income:any,outcome:any){
//     console.log('Update Chart Method called');
//     this.chart.data.datasets[0].data = [income, outcome];
//     this.chart.update();
//   }
// }







// Another way to show chart to not efftect until reload page
// import { Component, AfterViewInit, ViewChild, ElementRef, Inject, PLATFORM_ID } from '@angular/core';
// import { isPlatformBrowser } from '@angular/common';
// import { Chart } from 'chart.js/auto';

// @Component({
//   selector: 'app-income-outcome',
//   templateUrl: './income-outcome.html',
//   styleUrl: './income-outcome.scss',
// })
// export class IncomeOutcome implements AfterViewInit {

//   @ViewChild('donutCanvas') chartRef!: ElementRef;
//   chart: any;

//   constructor(@Inject(PLATFORM_ID) private platformId: Object) {}

//   ngAfterViewInit() {
//     // ✅ Only run in browser (NOT server)
//     if (isPlatformBrowser(this.platformId)) {
//       this.createChart();
//     }
//   }

//   createChart() {
//     if (this.chart) {
//       this.chart.destroy();
//     }

//     const ctx = this.chartRef.nativeElement.getContext('2d');

//     this.chart = new Chart(ctx, {
//       type: 'doughnut',
//       data: {
//         labels: ['Income', 'OutCome'],
//         datasets: [{
//           data: [1, 2],
//           backgroundColor: ['#e76f51', '#2a9d8f'],
//           hoverOffset: 20
//         }]
//       },
//       options: {
//         responsive: true
//       }
//     });
//   }
// }