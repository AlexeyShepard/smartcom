<template>
    <div>
        <b-button @click="Update()" variant="primary">Добавить</b-button>
        <b-tab title="Заказчики" active>
                <b-table striped hover :items="CustomerItems" :fields="fields">
                    <template #cell(Id)="data">
                        {{ data.item.id }}
                    </template>
                    <template #cell(Имя)="data">
                        {{ GetUserName(data.item.userId) }}
                    </template>
                    <template #cell(Адрес)="data">
                        {{ data.item.adress }}
                    </template>
                    <template #cell(Код)="data">
                        {{ data.item.code }}
                    </template>
                    <template #cell(Скидка)="data">
                        {{ data.item.discount }}
                    </template>
                    <template #cell(Действия)="row">
                        <b-button variant="primary" size="sm">Редактировать</b-button>
                        <b-button variant="danger" size="sm" @click="DeleteCustomer(row.item.id)">Удалить</b-button>
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
            fields:[
                'Id',
                'Имя',
                'Адрес',
                'Код',
                'Скидка',
                'Действия'
            ],
            CustomerItems: []
        }
    },
    created() {
        axios.get('http://localhost:11549/Customer',
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
                    this.CustomerItems = response.data;                                    
                })
                .catch(function (error) {
                        alert('ERROR ' + error);
                });       
    },
    methods: {
        async GetUserName(id) {
            
            var Name = '';

            await axios.get('http://localhost:11549/User/' + id,
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
                    Name = response.data.name;                                  
                })
                .catch(function (error) {
                        alert('ERROR ' + error);
                });      
                
                return Name;
        },

        async DeleteCustomer(id) {
            await axios.delete('http://localhost:11549/Customer/' + id,
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
    }
}

</script>
