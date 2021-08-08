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
                            <p>Тут пуфто!</p>
                            <b-button class="mt-3" variant="outline-warning" block @click="MakeOrder">Оформить заказ</b-button>
                            <b-button class="mt-3" variant="outline-danger" block @click="ClearOrder">Очистить корзину</b-button>
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

export default {
    methods: {
        IsAuth() {
            if(localStorage.getItem("Token") != undefined) return true;
        },
        Logout() {
            localStorage.clear();
            router.push("auth");
        },
        ShowModal() {
            this.$refs['modal-1'].show()
        },
        ClearOrder() {

        },
        IsCustomer() {
            if(localStorage.getItem("RoleId") == 2) return true;   
        },
        GetUserName() {
            return localStorage.getItem("Name")
        }
    }
}
</script>
