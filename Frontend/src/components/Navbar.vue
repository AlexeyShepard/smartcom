<!--Навбар сайта-->
<template>
    <div>
        <b-navbar toggleable="lg" type="dark" class="navb">

            <b-navbar-brand class="brand" href="#">Smartcom</b-navbar-brand>

            <b-collapse id="nav-text-collapse" is-nav>
                <b-navbar-nav>
                    <b-nav-text>Тестовое задание</b-nav-text>                   
                </b-navbar-nav>
            </b-collapse>

            <div v-if="IsAuth()">
                Вы авторизованы, как {{ GetUserName() }}
                <b-button variant="primary" @click="Logout">Выйти</b-button>  
                
                <div v-if="IsCustomer()">
                    <div>
                        <b-button id="show-btn" @click="ShowModal" variant="primary">Корзина</b-button>

                        <b-modal ref="modal-1" hide-footer title="Корзина">
                            <div v-if="IsEmpty()">
                                <p>Тут пуфто!</p>
                            </div>
                            <div v-else>
                                <b-table striped hover :items="BusketItems" :fields="fields">
                                    <template #cell(Наименование)="data">
                                        {{ data.item.name }}
                                    </template>
                                    <template #cell(Цена)="data">
                                        {{ data.item.price }}
                                    </template>
                                    <template #cell(Действия)="data">
                                        <b-button variant="danger" size="sm" @click="RemoveFromBusket(data.item.id)">Удалить</b-button>    
                                    </template>
                                </b-table>
                                <b-button class="mt-3" variant="outline-warning" block @click="MakeOrder">Оформить заказ</b-button>
                                <b-button class="mt-3" variant="outline-danger" block @click="ClearOrder">Очистить корзину</b-button>
                            </div>                                                      
                        </b-modal>
                    </div>
                </div>                         
            </div >
        </b-navbar>
    </div>
</template>

<style>
    .navb {
        background-color: rgb(107, 150, 230); 
    }

    .brand {
        margin-left: 10px;
    }
</style>

<script>

import router from '@/router';
import axios from 'axios';

export default {    
    data() {
        return {
            fields: [
                'Наименование',
                'Цена',
                'Действия'
            ],
            BusketItems: []
        }
    },
    methods: {
        IsAuth() {
            if(localStorage.getItem("Token") != undefined) return true;
        },
        IsEmpty() {
            if(localStorage.getItem("Busket") == "") return true;
        },
        Logout() {
            localStorage.clear();
            router.push("/");
        },
        ShowModal() {
            this.$refs['modal-1'].show()
        },
        RemoveFromBusket(id) {
            var Busket = JSON.parse("[" + localStorage.getItem("Busket") + "]");
            var index = Busket.indexOf(id);
            if (index !== -1) {
                Busket.splice(index, 1);
            }

            localStorage.removeItem("Busket");
            localStorage.setItem("Busket", Busket);

            location.reload();
        },
        ClearOrder() {
            localStorage.setItem("Busket", []);
            location.reload();
        },
        MakeOrder() {
            var Busket = JSON.parse("[" + localStorage.getItem("Busket") + "]");
            axios.post(this.$ApiUrl + '/Order', Busket,
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
                .then(function(response) {
                        alert("Заказ успешно оформлен");             
                })
                .catch(function (error) {
                        alert(error);
                });

            this.ClearOrder();
            location.reload();
        },
        IsCustomer() {
            if(localStorage.getItem("RoleId") == 2) return true;   
        },
        GetUserName() {
            return localStorage.getItem("Name")
        }
    },
    created() {
        if(localStorage.getItem("RoleId") == 2)
        {
            var Busket = JSON.parse("[" + localStorage.getItem("Busket") + "]");
            for(var Key in Busket) {
                axios.get(this.$ApiUrl + '/Product/' + Busket[Key],
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
                        this.BusketItems.push(response.data);                                   
                    })
                    .catch(function (error) {
                            alert('ERROR ' + error);
                    });   
            }
        }               
    }
}
</script>
