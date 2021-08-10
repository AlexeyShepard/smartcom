<template>
    <div>
        <b-tab title="Мои заказы">
            <b-table striped hover :items="OrderItems" :fields="fields">
                <template #cell(Номер_заказа)="data">
                    {{ data.item.order_Number }}
                </template>   
                <template #cell(Дата_оформления)="data">
                    {{ data.item.order_Date }}
                </template>  
                <template #cell(Завершение)="data">
                    {{ data.item.shipment_Date }}
                </template>
                <template #cell(Статус)="data">
                    {{ data.item.statusName }}
                </template>
                <template #cell(Действия)="data">
                    <div v-if="CheckStatus(data.item.statusId)">
                        <b-button variant="danger" size="sm" @click="CancelOrder(data.item.id)">Отменить</b-button>
                    </div>                    
                </template>
            </b-table>  
        </b-tab>
    </div>
</template>

<script>

import axios from 'axios';

export default {
    data() {
        return {
            fields: [
                'Номер_заказа',
                'Дата_оформления',
                'Завершение',
                'Статус',
                'Действия'
            ],
            OrderItems: []
        }
    },
    created() {
        axios.get(this.$ApiUrl + '/Order/customer',
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
    },
    methods: {
        async CancelOrder(id) {
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
            .then((response) => {
                alert("Заказ успешно отменён!");                                        
            })
            .catch(function (error) {
                alert('ERROR ' + error);
            });
                
            location.reload();   
        },
        CheckStatus(statusId) {
            if(statusId == 1) return true;
            else return false;
        }
    }
}

</script>
