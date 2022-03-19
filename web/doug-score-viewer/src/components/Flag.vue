<template>
  <div class="tooltip">
    <span class="tooltiptext">{{ props.country.displayName }}</span>
    <figure class="image is-32x32">
      <img :src="flagIconUrl" :alt="props.country.displayName" />
    </figure>
  </div>
</template>
<script setup lang="ts">
import { computed, PropType } from "vue";
import { Country } from "../models/country";
import { getFlagIcon } from "../utils";
const props = defineProps({
  country: {
    required: true,
    type: Object as PropType<Country>,
  },
});

const flagIconUrl = computed(() => {
  return getFlagIcon(props.country.flagIconName);
});
</script>
<style lang="scss"></style>
