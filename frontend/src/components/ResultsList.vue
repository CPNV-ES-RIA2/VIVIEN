<script setup>
import { computed } from 'vue';
import { useI18n } from 'vue-i18n';

const { t } = useI18n();

const labelDescriptionHeader = computed(() => {return t('labelDescriptionHeader')});
const labelConfidenceHeader = computed(() => {return t('labelConfidenceHeader')});

const props = defineProps({
    results: {
        type: Object,
        required: true
    }
})

</script>
<template>
    <table class="table table-bordered table-striped">
        <thead>
            <tr>
                <th scope="col">{{ labelDescriptionHeader }}</th>
                <th scope="col">{{ labelConfidenceHeader }}</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="result in results">
                <td>{{ result.name }}</td>
                <td>{{ result.confidence }}</td>
            </tr>
        </tbody>
    </table>
</template>