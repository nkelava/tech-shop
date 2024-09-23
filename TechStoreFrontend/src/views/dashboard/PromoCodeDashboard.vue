<script setup>
import { onMounted, ref } from "vue";
import { axiosPrivate } from "@/api/axios";
import PromoCodeForm from "@/components/admin/promo_code/PromoCodeForm.vue";
import PromoCodeList from "@/components/admin/promo_code/PromoCodeList.vue";

const promoCodes = ref([]);
const selectedPromoCodeId = ref(null);

onMounted(() => reloadPromoCodes());

async function reloadPromoCodes() {
  const resp = await axiosPrivate.get("/promo-codes").catch((error) => console.log(error));

  if (resp?.status !== 200) return;

  promoCodes.value = resp.data;
}

function editPromoCode(id) {
  selectedPromoCodeId.value = id;
}
</script>

<template>
  <div>
    <promo-code-form
      :id="selectedPromoCodeId"
      @reload="reloadPromoCodes"
      @clearSelectedId="selectedPromoCodeId = null"
    />
    <promo-code-list :promoCodes="promoCodes" @reload="reloadPromoCodes" @edit="editPromoCode" />
  </div>
</template>
