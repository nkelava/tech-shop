<script setup>
import { onMounted, ref } from "vue";
import { axiosPrivate } from "@/api/axios";
import PromoCodeForm from "@/components/admin/promo_code/PromoCodeForm.vue";
import PromoCodeList from "@/components/admin/promo_code/PromoCodeList.vue";

const promoCodes = ref([]);

onMounted(() => reloadPromoCodes());

async function reloadPromoCodes() {
  const resp = await axiosPrivate.get("/promo-codes").catch((error) => console.log(error));

  if (resp.status !== 200) return;

  promoCodes.value = resp.data;
}
</script>

<template>
  <div>
    <promo-code-form @reload="reloadPromoCodes" />
    <promo-code-list @reload="reloadPromoCodes" :promoCodes="promoCodes" />
  </div>
</template>
