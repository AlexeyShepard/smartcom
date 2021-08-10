<!--Таблица редактирования заказов-->
<template>
    <div>
        <b-tab title="Заказы">
            <b-table id="OrderTable"
            striped hover 
            :items="OrderItems" 
            :fields="fields"
            :per-page="PerPage"
             :current-page="CurrentPage">
                <template #cell(Id)="data">
                    {{ data.item.id }}
                </template>
                <template #cell(Дата_оформления)="data">
                    {{ data.item.order_Date }}
                </template>
                <template #cell(Завершение)="data">
                    {{ data.item.shipment_Date }}
                </template>
                <template #cell(Номер_заказа)="data">
                    {{ data.item.order_Number }}
                </template>
                <template #cell(Имя_заказчика)="data">
                    {{ data.item.userName }}
                </template>
                <template #cell(Статус)="data">
                    {{ data.item.statusName }}
                </template>
                <template #cell(Действия)="data">
                    <b-button variant="primary" size="sm" v-b-modal.modal-edit-order @click="OpenEditOrderModal(data.item)">Редактировать</b-button>
                    <b-button variant="danger" size="sm" @click="DeleteOrder(data.item.id)">Удалить</b-button>
                </template>
            </b-table>
            <b-pagination
                    v-model="CurrentPage"
                    :total-rows="rows"
                    :per-page="PerPage"
                    aria-controls="OrderTable">
            </b-pagination>   
        </b-tab>
        <!--Редактирование заказа-->
        <b-modal id="modal-edit-order" title="Редактирование заказа" 
                hide-footer
                has-modal-card
                trap-focus
                :destroy-on-hide="false"
                aria-role="dialog"
                aria-modal>
                <form action="">
                    <div class="modal-card" style="width: 450px">
                        <section class="modal-card-body">
                            <b-field label="Дата завершения">
                                <b-input
                                    type="date"
                                    :value="shipment_Date"
                                    password-reveal
                                    placeholder="Наименование"
                                    required
                                    v-model="shipment_Date">
                                </b-input>
                            </b-field>

                            <b-field label="Статус">
                                <b-form-select
                                    :options="Statuses"
                                    v-model="statusId"
                                    required>
                                </b-form-select>
                            </b-field>                            
                        </section>
                    <footer class="modal-card-foot">
                        <b-button class="mt-3" variant="outline-primary" block @click="EditOrder">Изменить</b-button>
                    </footer>
                </div>       
                </form>            
        </b-modal>
    </div>
</template>

<script>

import axios from 'axios';

export default {
    data() {
        return {
            id:0,
            order_Date: null,
            shipment_Date: null,
            order_number: 0,
            customerId: 0,
            statusId: 0,
            CurrentPage:1,
            PerPage:15,
            fields: [
                'Id',
                'Дата_оформления',
                'Завершение',
                'Номер_заказа',
                'Имя_заказчика',
                'Статус',
                'Действия'
            ],
            OrderItems: [],
            Statuses: []
        }
    },
    created() { 
        axios.get(this.$ApiUrl + '/Order',
                {
                    headers : {
                        'Content-Type':'application/json',
                        'Accept':'application/json',
                        'Authorization':'Bearer ' + localStorage.getItem("Token")
                    }
                },
                {
                    withCredentials: true
                })
                .then((response) => {
                    this.OrderItems = response.data;                                    
                })
                .catch(function (error) {
                        alert('ERROR ' + error);
                });
        axios.get(this.$ApiUrl + '/Order/Status',
                {
                    headers : {
                        'Content-Type':'application/json',
                        'Accept':'application/json',
                        'Authorization':'Bearer ' + localStorage.getItem("Token")
                    }
                },
                {
                    withCredentials: true
                })
                .then((response) => {
                    this.Statuses = response.data;                                    
                })
                .catch(function (error) {
                        alert('ERROR ' + error);
                });    
    },
    computed: {
      rows() {
        return this.OrderItems.length
      }
    },
    methods: {
        async DeleteOrder(id) {
        
        await axios.delete(this.$ApiUrl+ '/Order/' + id,
            {
                headers : {
                    'Content-Type':'application/json',
                    'Accept':'application/json',
                    'Authorization':'Bearer ' + localStorage.getItem("Token")
                }
            },
            {
                withCredentials: true
            })
            .catch(function (error) {
                alert('ERROR ' + error);
            });
                
            location.reload();
        },
        OpenEditOrderModal(OrderData) {
            this.id = OrderData.id,
            this.order_Date = OrderData.order_Date,
            this.shipment_Date = OrderData.shipment_Date,
            this.order_number = OrderData.order_number,
            this.customerId = OrderData.customerId,
            this.statusId = OrderData.statusId
        },
        async EditOrder() {
            await axios.put(this.$ApiUrl + '/Order', {
                    id: this.id,
                    order_Date: this.order_Date,
                    shipment_Date: this.shipment_Date,
                    order_number: this.order_number,
                    customerId: this.customerId,
                    statusId: this.statusId
                },
                {
                    headers : {
                        'Content-Type':'application/json',
                        'Accept':'application/json',
                        'Authorization':'Bearer ' + localStorage.getItem("Token")
                    }
                },
                {
                    withCredentials: true
                })
                .then((response) => {
                    alert("Заказ успешно изменен!");                                       
                })
                .catch(function (error) {
                        alert('ERROR ' + error);
                });

                location.reload();
        }
    }
}

</script>
